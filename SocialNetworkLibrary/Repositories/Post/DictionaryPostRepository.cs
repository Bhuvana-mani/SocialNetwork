using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SocialNetworkLibrary.Models.Posts;
using SocialNetworkLibrary.Models.Users;



namespace SocialNetworkLibrary.Repositories.Users
{
    public class DictionaryPostRepository : IPostRepository
    {
       
        private readonly Dictionary<int, Post> _posts = new Dictionary<int, Post>();
/// <summary>
/// intilaize posts
/// </summary>
/// <param name="userRepository"></param>
        public DictionaryPostRepository(IUserRepository userRepository)
        {
            var user = userRepository.GetUserById(1);
            var post = new Post(1)
            {
                Description = "Freezing Winters",
                CreatedBy = user,
                
                
            };
            var user1 = userRepository.GetUserById(2);
            var post1 = new Post(2)
            {
                Description = "Summer Activities in sweden",
                CreatedBy = user1,
               
                
            };
            _posts.Add(1, post);
            _posts.Add(2, post1);
        }
        /// <summary>
        /// fetches posts with their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Post GetPostWithId(int id)
        {
            _posts.TryGetValue(id, out Post result);
            return result;
        }

            
            
        public IEnumerable<Post> GetPosts()
        {
            return _posts.Values;
        }
        /// <summary>
        /// create a new post by a registerd user
        /// </summary>
        /// <param name="postDto"></param>
        /// <param name="user"></param>
        
        /// <returns></returns>
        public Post Add(PostDto postDto, User user)
        {
            var id = _posts.Count + 1;
            var post = new Post(id, postDto, user);
            _posts.Add(id, post);
            return post;
        }
/// <summary>
/// update an existing post by fetching its id
/// </summary>
/// <param name="post"></param>
        public void Update(Post post)
        {
            _posts.Remove(post.Id);
            _posts.Add(post.Id, post);
        }

        public void ApplyPatch(Post post, Dictionary<string, object> patches)
        {
            ApplyPatch<Post>(post, patches);
            post.LastUpdated = DateTime.Now;
        }
/// <summary>
/// Delete a post by identfying its Id
/// </summary>
/// <param name="post"></param>
        public void Delete(Post post)
        {
            _posts.Remove(post.Id);
        }
        public void LikePost(Post post, User user)
        {
            post.UserLikes.Add(user);
            Update(post);
        }
        public void UnLikePost(Post post, User user)
        {
            post.UserLikes.Remove(user);
            Update(post);
        }

        private void ApplyPatch<T>(T original, Dictionary<string, object> patches)
        {
            var properties = original.GetType().GetProperties();
            foreach (var patch in patches)
            {
                foreach (var prop in properties)
                {
                    if (string.Equals(patch.Key, prop.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        prop.SetValue(original, patch.Value);
                    }
                }
            }
        }

        public object Add(PostDto postDto, IEnumerable<User> user)
        {
            throw new NotImplementedException();
        }

        void IPostRepository.ApplyPatch<T>(T original, Dictionary<string, object> patches)
        {
            throw new NotImplementedException();
        }
    }
}

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
            var user = userRepository.GetUser(1);
            var post = new Post(1)
            {
                Description = "Freezing Winters",
                CreatedBy = user,
                
            };
            var user1 = userRepository.GetUser(2);
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

       /// <summary>
       /// fetch all the dependencies in for the post by identifying their Id
       /// </summary>
       /// <param name="postId"></param>
       /// <returns></returns>
        public IEnumerable<Post> GetDependeciesForPost(int postId)
        {
            _posts.TryGetValue(postId, out Post result);
            return result?.Dependencies ?? null;
        }

       
        public IEnumerable<Post> GetPostsCreatedBy(string createdBy)
        {
            throw new NotImplementedException();
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
        /// <param name="dependecies"></param>
        /// <returns></returns>
        public Post Add(PostDto postDto, User user, List<Post> dependecies)
        {
            var id = _posts.Count + 1;
            var post = new Post(id, postDto, dependecies, user);
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
        }
/// <summary>
/// Delete a post by identfying its Id
/// </summary>
/// <param name="post"></param>
        public void Delete(Post post)
        {
            _posts.Remove(post.Id);
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

        public object Add(PostDto postDto, IEnumerable<User> user, List<Post> dependecies)
        {
            throw new NotImplementedException();
        }
    }
}

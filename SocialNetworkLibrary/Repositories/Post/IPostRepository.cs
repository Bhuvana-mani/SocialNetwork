using System;
using System.Collections.Generic;
using System.Text;
using SocialNetworkLibrary.Models.Posts;
using SocialNetworkLibrary.Models.Users;

namespace SocialNetworkLibrary.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetDependeciesForPost(int postId);

        Post GetPostWithId(int id);

        IEnumerable<Post> GetPostsCreatedBy(string createdBy);

        IEnumerable<Post> GetPosts();

        Post Add(PostDto post, User user, List<Post> dependecies);

        void Update(Post post);

        void ApplyPatch(Post post, Dictionary<string, object> patches);

        void Delete(Post post);
        //object Add(PostDto postDto, IEnumerable<User> user, List<Post> dependecies);
    }
}

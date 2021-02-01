using System;
using System.Collections.Generic;
using System.Text;
using SocialNetworkLibrary.Models.Posts;
using SocialNetworkLibrary.Models.Users;

namespace SocialNetworkLibrary.Repositories
{
    public interface IPostRepository
    {
       

        IEnumerable<Post> GetPosts();
        Post GetPostWithId(int id);
        Post Add(PostDto post, User user);
        void Update(Post post);
        void ApplyPatch(Post post, Dictionary<string, object> patches);
        void Delete(Post post);
        void LikePost(Post post, User user);
        void UnLikePost(Post post, User user);
        void ApplyPatch<T>(T original, Dictionary<string, object> patches);
    }
}

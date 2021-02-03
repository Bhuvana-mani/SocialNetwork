using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using SocialNetworkLibrary.Models.Posts;
using SocialNetworkLibrary.Repositories;
using SocialNetworkLibrary.Repositories.Users;

namespace SocialNetwork.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostsController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        /// <summary>
        /// Fetches all existing posts.
        /// </summary>
        /// <returns>All existing posts</returns>
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        /// <summary>
        /// fetched all the posts with Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Post> GetPost(int id)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
                return NotFound(post);
            return post;
        }
        
        /// <summary>
        /// helps to create a new post
        /// </summary>
        /// <param name="postDto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreatePost(PostDto postDto)
        {
            try
            {
                var user = _userRepository.GetUserById(postDto.CreatedBy);
               
                var post = _postRepository.Add(postDto, user);
                return CreatedAtAction(nameof(GetPost), routeValues: new { id = post.Id }, value: post);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
        ///// <summary>
        ///// Replace the content of  an existing post
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="postPut"></param>
        ///// <returns></returns>
        //[HttpPut]
        //[Route("{id:int}")]
        //public ActionResult ReplacePost(int id, PostDto postPut)
        //{
        //    var post = _postRepository.GetPostWithId(id);
        //    if (post is null)
        //        return NotFound($"No post with {id} found");
        //    var putPost = new Post(id, postPut, null);
        //    _postRepository.Update(putPost);
           
        //    return NoContent();
        //}
        /// <summary>
        /// Update an existing post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patches"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id:int}/update/{createdBy:int}")]
        public ActionResult UpdatePost(int id, Dictionary<string, object> patches,int createdBy)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
            {

                return NotFound($"No post with {id} found");
            }
            var user = _userRepository.GetUserById(createdBy);
            if (user is null)
            {
                return NotFound($"User with id {createdBy} not found");
            }
            if (post.CreatedBy != user)
            {
                return BadRequest($"User with id {createdBy} is unauthorized for this request");
            }
            _postRepository.ApplyPatch(post, patches);
            return NoContent();
        }

        /// <summary>
        /// Deletes a post
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public ActionResult DeletePost(int id)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
                return NotFound($"No post with {id} found");
            _postRepository.Delete(post);
            return NoContent();
        }
        /// <summary>
        /// Add a like to the post, a person can do only like once
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/like/{createdBy:int}")]
        public ActionResult LikePost(int id, int createdBy)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
            {
                return NotFound($"Post with id {id} not found");
            }
            var user = _userRepository.GetUserById(createdBy);
            if (user is null)
            {
                return NotFound($"User with id {createdBy} not found");
            }
            var like = post.UserLikes.Contains(user);
            if (like)
            {
                return BadRequest($"User with id {createdBy} is unauthorized for this request");
            }
            _postRepository.LikePost(post, user);
            return NoContent();
           
        }
        /// <summary>
        /// Remove unlike from the post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}/unlike/{createdBy:int}")]
        public ActionResult UnLikePost(int id,int createdBy)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
            {
                return NotFound($"Post with id {id} not found");
            }
            var user = _userRepository.GetUserById(createdBy);
            if (user is null)
            {
                return NotFound($"User with id {createdBy} not found");
            }
            var unlike = post.UserLikes.Contains(user);
            if (!unlike)
            {
                return BadRequest($"User with id {createdBy} is unauthorized for this request");
            }
            _postRepository.UnLikePost(post, user);
            return NoContent();

        }


       
    }
}

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
        /// 
        /// </summary>
        /// <param name="postQuery"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Post> GetAllQueried([FromQuery] PostQueryDto postQuery)
        {
            return RunPostQuery(postQuery);
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
                var user = _userRepository.GetUser(postDto.CreatedBy);
               
                var post = _postRepository.Add(postDto, user);
                return CreatedAtAction(nameof(GetPost), routeValues: new { id = post.Id }, value: post);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Replace the content of  an existing post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postPut"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public ActionResult ReplacePost(int id, PostDto postPut)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
                return NotFound($"No post with {id} found");
            var putPost = new Post(id, postPut, null);
            _postRepository.Update(putPost);
            return NoContent();
        }
        /// <summary>
        /// Update an existing post
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patches"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id:int}")]
        public ActionResult UpdatePost(int id, Dictionary<string, object> patches)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
                return NotFound($"No post with {id} found");
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
        [Route("{id:int}/like/{createdby:string}")]
        public ActionResult LikePost(int id, string createdBy)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
            {
                return NotFound($"Post with id {id} not found");
            }
            var user = _userRepository.GetUser(createdBy);
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
        [Route("{id:int}/unlike/{createdby:string}")]
        public ActionResult UnLikePost(int id, string createdBy)
        {
            var post = _postRepository.GetPostWithId(id);
            if (post is null)
            {
                return NotFound($"Post with id {id} not found");
            }
            var user = _userRepository.GetUser(createdBy);
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


        private IEnumerable<Post> RunPostQuery(PostQueryDto postQueryDto)
        {
            if (postQueryDto.IsEmpty)
                return _postRepository.GetPosts();
            
            else if (!(postQueryDto.CreatedBy is null))
                return _postRepository.GetPostsCreatedBy(postQueryDto.CreatedBy);
            
            else
                throw new NotSupportedException("The query combination selected is not supported");
        }
    }
}

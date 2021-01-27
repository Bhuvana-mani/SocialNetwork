using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SocialNetworkLibrary.Models.Users;

namespace SocialNetworkLibrary.Models.Posts
{
    public class Post
    {
        private const string _stringMessage = "{0} must be between {2} and {1} characters long";
        /// <summary>
        /// fields required for the post
        /// </summary>
        /// <param name="id"></param>
        public Post(int id)
        {
            Id = id;
        }

        public Post(int id, PostDto postDto, List<Post> depedecies, User user)
        {
            Id = id;
            Description = postDto.Description;
            Dependencies = depedecies;
            CreatedBy = user;
        }

        public int Id { get; private set; }
/// <summary>
/// specify the length of the message
/// </summary>
        [Required]
        [StringLength(100, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public User CreatedBy { get; set; }

        [MaxLength(10)]
        public string[] Comments { get; set; } = new string[0];

        
        public List<Post> Dependencies { get; set; } = new List<Post>();

    }
}

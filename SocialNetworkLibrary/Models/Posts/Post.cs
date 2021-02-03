using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SocialNetworkLibrary.Models.Users;

namespace SocialNetworkLibrary.Models.Posts
{
    public class Post
    {
       
       
        /// <summary>
        /// fields required for the post
        /// </summary>
        /// <param name="id"></param>
        public Post(int id)
        {
            Id = id;
        }

        public Post(int id, PostDto postDto, User user)
        {
            Id = id;
            Description = postDto.Description;
            LastUpdated = postDto.LastUpdated;
            CreatedBy = user;
        }

        public int Id { get; private set; }
/// <summary>
/// specify the length of the message
/// </summary>
        [Required]
       
        public string Description { get; set; }

        [Required]
        public User CreatedBy { get; set; }

        [MaxLength(10)]
        public string[] Comments { get; set; } = new string[0];

       
        public DateTime LastUpdated { get; set; }

       
        public List<User>UserLikes  { get; set; } = new List<User>();

    }
}

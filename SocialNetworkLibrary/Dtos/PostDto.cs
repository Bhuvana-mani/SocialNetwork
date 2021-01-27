using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkLibrary.Models.Posts
{
    public class PostDto
    {
        private const string _stringMessage = "{0} must be between {2} and {1} characters long";

        [Required]
        [StringLength(100, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [MaxLength(4)]
        public string[] Comments { get; set; } = new string[0];

        public List<int> Dependencies { get; set; } = new List<int>();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkLibrary.Models.Posts
{
    public class PostDto
    {
        private const string _rangeMessage = "{0} must be between {1} and {2}";
        private const string _stringMessage = "{0} must be between {2} and {1} characters long";

        [Required]
        [StringLength(100, ErrorMessage = _stringMessage, MinimumLength = 5)]
        public string Description { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Range(typeof(DateTime), "1/1/2020", "1/1/2021", ErrorMessage = _rangeMessage)]
        public DateTime LastDate { get; set; }

        [MaxLength(4)]
        public string[] Comments { get; set; } = new string[0];

       
    }
}

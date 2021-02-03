using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkLibrary.Models.Posts
{
    public class PostDto
    {
       
        

        [Required]
        
        public string Description { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        
        public DateTime LastUpdated { get; set; }

        [MaxLength(4)]
        public string[] Comments { get; set; } = new string[0];

       
    }
}

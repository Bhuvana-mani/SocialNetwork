using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkLibrary.Models.Posts
{
   
        public class PostQueryDto
        {
       
        public string CreatedBy { get; set; }

        public bool IsEmpty =>  CreatedBy is null;

    }
    
}

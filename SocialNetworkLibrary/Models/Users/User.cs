﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SocialNetworkLibrary.Models.Users
{/// <summary>
/// the user profile
/// </summary>
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email{ get; set; }

    }
}

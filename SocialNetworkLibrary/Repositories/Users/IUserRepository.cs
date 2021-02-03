using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SocialNetworkLibrary.Models.Users;


namespace SocialNetworkLibrary.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);

        User GetUserById(int id);
        bool UserNameIsNotUnique(User user);
        IEnumerable<User> GetUsers();
    }
}

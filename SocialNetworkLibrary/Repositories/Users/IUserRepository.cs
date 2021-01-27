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

        User GetUser(int id);
        User GetUser(string createdby);
        IEnumerable<User> GetUsers(string createdBy);
        bool UserNameIsUnique(User user);
        IEnumerable<User> GetUsers();
    }
}

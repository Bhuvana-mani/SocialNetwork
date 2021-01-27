using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SocialNetworkLibrary.Models.Users;

namespace SocialNetworkLibrary.Repositories.Users
{
    public class DictionaryUserRepository : IUserRepository
    {
        private readonly Dictionary<int, User> _users = new Dictionary<int, User>();

        public DictionaryUserRepository()
        {
            var user = new User
            {
                Id = 1,
                UserName = "Bhuvana",
                Email = "buwa@hmail.com",
                
            };
            var user1 = new User
            {
                Id = 2,
                UserName = "Bhuvi",
                Email = "bhuvi@hmail.com",

            };
            _users.Add(1, user);
            _users.Add(2, user1);
        }
       

        public bool UserNameIsUnique(User user)
        {
            return _users.Any(e => e.Value.UserName == user.UserName);
        }

        public void Add(User user)
        {
            if (!UserNameIsUnique(user))
            {
                throw new NonUniqueUserName();
            }
            if (_users.ContainsKey(user.Id))
            {
                throw new NonUniqueId();
            }
            _users.Add(user.Id, user);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.Values;
        }

        public User GetUser(int id)
        {
            return _users[id];
        }

        public IEnumerable<User> GetUsers(string createdBy)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string createdby)
        {
            return _users.Single(e => e.Value.UserName == createdby).Value;
            
        }
    }
}

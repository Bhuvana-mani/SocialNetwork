using Microsoft.AspNetCore.Mvc;
using SocialNetworkLibrary.Models.Users;
using SocialNetworkLibrary.Repositories;
using SocialNetworkLibrary.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SocialNetwork.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
/// <summary>
/// Lists out all the user
/// </summary>
/// <returns></returns>
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }
        /// <summary>
        /// get user with Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<User> GetUser(int id)
        {
            try
            {
                return _userRepository.GetUser(id);
            }
            catch (ArgumentException)
            {
                return NotFound(id);
            }
        }
/// <summary>
/// creates a new user
/// </summary>
/// <param name="user"></param>
/// <returns></returns>
        [HttpPost]
        public ActionResult<User> CreateUser(User user)
        {
            try
            {
                _userRepository.Add(user);
                return user;
            }
            catch (UserException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

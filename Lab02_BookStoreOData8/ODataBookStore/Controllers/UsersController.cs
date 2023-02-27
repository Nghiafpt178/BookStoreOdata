using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataBookStore.DTOs;
using ODataBookStore.Models;
using ODataBookStore.Repository;
using ODataBookStore.Services;

namespace ODataBookStore.Controllers
{
	public class UsersController : ODataController
	{
        private readonly IUserRepository userRepository;

		public UsersController(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}
        [HttpGet]
        [EnableQuery]
        public IActionResult Get()
        {
            List<User> users = userRepository.GetAll();
            return Ok(users);
        }

        [EnableQuery]
        public IActionResult Get(string key)
        {
            UserRespond userRespond = userRepository.GetUserByID(key);
            return Ok(userRespond);
        }

        [EnableQuery]
        public IActionResult Post(string email, string pass)
        {
            User userCheck = userRepository.UserCheck(email, pass);
            if(userCheck == null)
            {
                return NotFound();
            }
            return Ok(new Respond<User>()
            {
                IsSuccess = true,
                Message = "User exist in db!",
                Data = userCheck
            });
        }

        [EnableQuery]
        [HttpPut]
        public IActionResult Put([FromODataUri] string key, [FromBody] UserRespond userRespond)
        {
            UserRespond userRespond1 = userRepository.UpdateUser(key, userRespond);
            if (userRespond1 == null)
            {
                return NotFound();
            }
            return Ok(new Respond<UserRespond>()
            {
                IsSuccess = true,
                Message = "Update user success!",
                Data = userRespond1
            });
        }
    }
}

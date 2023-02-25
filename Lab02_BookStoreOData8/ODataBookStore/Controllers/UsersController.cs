using Microsoft.AspNetCore.Mvc;
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
    }
}

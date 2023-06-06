using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HSPA_BACKEND.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
       
       

        public UserManagementController(UserManager<User> userManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
       
        }

        //register page for users to register with username and pwd

        [Authorize("Admin")]
        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = userManager.Users;
            var userDtoList = mapper.Map<IEnumerable<UserRegistrationDto>>(users);
            return Ok(userDtoList);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = mapper.Map<UserRegistrationDto>(user);
            return Ok(userDto);
        }

        
        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // User deleted successfully
                return Ok();
            }

            // Deletion failed, return errors
            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(errors);
        }

    }
}

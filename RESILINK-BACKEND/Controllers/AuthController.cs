using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;



namespace RESILINK_BACKEND.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AuthController(SignInManager<User> signInManager,RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = mapper.Map<User>(registrationDto);
            user.UserName = registrationDto.Email;

            var result = await userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                if (!await roleManager.RoleExistsAsync(registrationDto.RoleAlloted))
                    await roleManager.CreateAsync(new IdentityRole(registrationDto.RoleAlloted));

                if (await roleManager.RoleExistsAsync(registrationDto.RoleAlloted))
                    await userManager.AddToRoleAsync(user, registrationDto.RoleAlloted);
                // User registered successfully
                return Ok("User registered successfully");
            }

            // Registration failed, return errors
            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(errors);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto logindto)
        {
            var result = await signInManager.PasswordSignInAsync(logindto.Email, logindto.Password, false, false);
            if (result.Succeeded)
            {

                //return the JWT
                var theUser = await userManager.FindByEmailAsync(logindto.Email);
                var theRole = await userManager.GetRolesAsync(theUser);

                var token = GenerateToken(theUser.Id, theUser.UserName,String.Join(',', theRole));


                return Ok(token);
            }
            return BadRequest(new { message = "Invalid Username or Password" });
        }

        private string GenerateToken(string userId, string username, string roleInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("1234567890!@#$%^&*()ASDFGHJKLqwertyuiop");
            var expiresAt = DateTime.Now.AddDays(30);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim("Id", userId),
                        new Claim(ClaimTypes.Role, roleInfo)
                    }),
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}


/*using AutoMapper;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace RESILINK_BACKEND.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // Register page for users to register with username and password
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserRegistrationDto registrationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = mapper.Map<User>(registrationDto);

            var result = await userManager.CreateAsync(user, registrationDto.Password);

            if (result.Succeeded)
            {
                var userClaims = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    // Add any additional claims you want to include in the token
                });

                var token = GenerateToken(userClaims);

                // Return the generated token
                return Ok(new { Token = token });
            }

            // Registration failed, return errors
            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return BadRequest(new { Message = "Invalid credentials" });
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return BadRequest(new { Message = "Invalid credentials" });
            }

            var userClaims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                // Add any additional claims you want to include in the token
            });

            var token = GenerateToken(userClaims);

            // Return the generated token
            return Ok(new { Token = token });
        }

        public string GenerateToken(ClaimsIdentity claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("1234567890!@#$%^&*()ASDFGHJKLqwertyuiop");
            var expiresAt = DateTime.Now.AddDays(30);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
*/
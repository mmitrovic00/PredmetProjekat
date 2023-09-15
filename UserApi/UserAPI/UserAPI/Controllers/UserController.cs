using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebProjekat.DTO.User;
using WebProjekat.Interfaces;
using WebProjekat.Models.Enum;

namespace WebProjekat.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegistrationUserDto newUser)
        {
            TokenDTO token = _userService.Registration(newUser,out string mess);
            if (token != null)
                return Ok(token);
            else
                return BadRequest(mess);
        }

        [HttpGet("{email}")]
        [Authorize(Roles = "ADMIN,CUSTOMER,SELLER")]
        public IActionResult GetByEmail(string email)
        {
            if (!User.Identity.Name.Equals(email))
                return BadRequest("Niste uneli ispravan mejl");
            UserDto userInfo = _userService.GetByEmail(email);
            if (userInfo == null)
                return BadRequest("Korisnik ne postoji");
            return Ok(userInfo);
        }

        [HttpPost("login")]
        public IActionResult LogIn([FromBody] LogInDto user)
        {
            TokenDTO token = _userService.LogIn(user);
            if (token == null)
                return BadRequest("Korisnik ne postoji");
            else if (String.IsNullOrEmpty(token.Token))
                return BadRequest("Pogresna lozinka");

            return Ok(token);
        }

        [HttpPost("googleLogin")]
        public IActionResult GoogleLogIn([FromBody] GoogleLogInDto user)
        {
            TokenDTO token = _userService.GoogleLogInUser(user);
            if (token == null)
                return BadRequest("An account has already been registered with this email");

            return Ok(token);
        }
        [HttpPost("usrimgUpdate")]
        [Authorize(Roles = "SELLER,ADMIN,CUSTOMER")]
        public IActionResult UpdateImage(IFormFile file)
        {
            try
            {
                var result = _userService.UpdateUserImage(file, User.Identity.Name);
                if (!result)
                    return NotFound("You can only update your image");
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("No file uploaded");
            }
        }
        [HttpPost("update")]
        [Authorize(Roles = "ADMIN,CUSTOMER,SELLER")]
        public IActionResult UpdateUser([FromBody] UpdateUserDto user)
        {
            bool result = _userService.UpdateUser(user);
            if (result)
                return Ok();
            return BadRequest("Neispravan mejl");
        }

        [HttpPost("passwordChange")]
        [Authorize(Roles = "ADMIN,CUSTOMER,SELLER")]
        public IActionResult ChangePassword([FromBody] PasswordDto data)
        {
            
            bool result = _userService.ChangePassword(data, out string mess);
            if (result)
                return Ok();
            return BadRequest(mess);
        }

        [HttpGet("customers")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetCustomers()
        {
            return Ok(_userService.GetCustomers());
        }

        [HttpGet("sellers")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult GetSellers()
        {
            return Ok(_userService.GetSellers());
        }

        [HttpPost("verify/{email}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Verify(string email)
        {
            bool result = _userService.SetSellerStatus(email, ESeller.VERIFIED);
            if (result)
                return Ok();
            return BadRequest("Prodavac sa ovim imejlom ne postoji.");
        }

        [HttpPost("reject/{email}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Reject(string email)
        {
            bool result = _userService.SetSellerStatus(email, ESeller.REJECTED);
            if (result)
                return Ok();
            return BadRequest("Prodavac sa ovim imejlom ne postoji.");
        }

    }
}

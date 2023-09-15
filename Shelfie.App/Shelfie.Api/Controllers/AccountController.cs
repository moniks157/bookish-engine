using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shelfie.Api.Models;
using Shelfie.Domain.Commands;

namespace Shelfie.Api.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public static User user = new User();
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("regiser")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            var command  = new RegisterUserCommand(userDto.Username, userDto.Password);
            _mediator.Send(command);

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto userDto)
        {
            if(userDto.Username != user.Username)
            {
                return Unauthorized();
            }

            if(!VerifyPasswordHash(userDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized();
            }

            string token = "token";

            return Ok(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        //private string CreateToken(User user)
        //{
        //    //List<Claim> claims = new List<Claim>
        //    //{ 
        //    //    new Claim(ClaimTypes.Name, user.Username)
        //    //};

        //    //var key = new SymmetricSecurityKey(GetBytes())
        //}
    }
}

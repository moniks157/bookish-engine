using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Models;
using Shelfie.Domain.UseCases.LoginUser;
using Shelfie.Domain.UseCases.RegisterUser;

namespace Shelfie.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel registerUserModel)
        {
            var command  = _mapper.Map<RegisterUserCommand>(registerUserModel); 
            var user = await _mediator.Send(command);

            if (user == null)
                return BadRequest("User already exists");

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserModel loginUserModel)
        {
            var command = _mapper.Map<LoginUserCommand>(loginUserModel);
            var token = await _mediator.Send(command);

            if (token == null)
                return BadRequest("Invalid credentials");

            return Ok(token);
        }
    }
}

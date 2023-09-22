using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Api.Models;
using Shelfie.Domain.Commands.LoginUser;
using Shelfie.Domain.Commands.RegisterUser;

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
        public IActionResult Register([FromBody] RegisterUserModel registerUserModel)
        {
            var command  = _mapper.Map<RegisterUserCommand>(registerUserModel); 
            var user = _mediator.Send(command);
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserModel loginUserModel)
        {
            var command = _mapper.Map<LoginUserCommand>(loginUserModel);
            var token = _mediator.Send(command);
            return Ok(token);
        }
    }
}

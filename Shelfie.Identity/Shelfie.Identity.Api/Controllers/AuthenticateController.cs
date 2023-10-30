using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shelfie.Identity.Api.Dtos;
using Shelfie.Identity.BusinessLogic.UseCases.LoginUser;
using Shelfie.Identity.BusinessLogic.UseCases.RegisterUser;

namespace Shelfie.Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticateController(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var request = new LoginUserCommand
        {
            Username = model.Username,
            Password = model.Password
        };

        var result = await _mediator.Send(request);

        if (!result.IsSuccess)
        {
            if(result.ErrorCode == BusinessLogic.Enums.ErrorCode.InvalidRequest)
            {
                return BadRequest(result.ValidationErrors);
            }
            return BadRequest(Messages.InvalidUsernameOrPassword);
        }

        return Ok(result.Data);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
    {
        var request = new RegisterUserCommand
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password
        };

        var result = await _mediator.Send(request);

        if(!result.IsSuccess)
        {
            if (result.ErrorCode == BusinessLogic.Enums.ErrorCode.InvalidRequest)
            {
                return BadRequest(result.ValidationErrors);
            }
            return BadRequest(result.ErrorCode == BusinessLogic.Enums.ErrorCode.EntityAlreadyExists ? Messages.UserAlreadyExists : Messages.UserCreationFailed);
        }

        return NoContent();
    }

    [HttpPost]
    [Route("external-login")]
    public async Task<IActionResult> ExternalLogin()
    {

        return Ok();
    }
}

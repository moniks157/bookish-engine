﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shelfie.Identity.Api.Models;
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
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var request = new LoginUserCommand
        {
            Username = model.Username,
            Password = model.Password
        };

        var result = await _mediator.Send(request);

        if (result is null)
        {
            return BadRequest(Messages.InvalidUsernameOrPassword);
        }

        return Ok(result);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var request = new RegisterUserCommand
        {
            Username = model.Username,
            Email = model.Email,
            Password = model.Password
        };

        var result = await _mediator.Send(request);

        if(result is null)
        {
            return BadRequest(Messages.UserAlreadyExists);
        }

        if (!result.Succeeded)
        {
            return BadRequest(Messages.UserCreationFailed);
        }

        return NoContent();
    }
}

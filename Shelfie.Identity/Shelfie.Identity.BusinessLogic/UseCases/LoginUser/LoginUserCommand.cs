﻿using MediatR;

namespace Shelfie.Identity.BusinessLogic.UseCases.LoginUser;

public class LoginUserCommand : IRequest<string?>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Shelfie.Identity.BusinessLogic.UseCases.RegisterUser;

public class RegisterUserCommand : IRequest<IdentityResult>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

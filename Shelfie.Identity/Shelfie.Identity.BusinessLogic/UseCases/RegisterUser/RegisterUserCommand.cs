using MediatR;
using Shelfie.Identity.BusinessLogic.Models;

namespace Shelfie.Identity.BusinessLogic.UseCases.RegisterUser;

public class RegisterUserCommand : IRequest<OperationResult<string?>>
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}

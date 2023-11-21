using MediatR;
using Shelfie.Identity.BusinessLogic.Models;

namespace Shelfie.Identity.BusinessLogic.UseCases.LoginUser;

public class LoginUserCommand : IRequest<OperationResult<string?>>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}

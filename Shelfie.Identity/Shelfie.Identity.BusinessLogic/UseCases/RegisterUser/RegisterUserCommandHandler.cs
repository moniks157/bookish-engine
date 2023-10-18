using MediatR;
using Microsoft.AspNetCore.Identity;
using Shelfie.Identity.BusinessLogic.Enums;
using Shelfie.Identity.BusinessLogic.Models;
using Shelfie.Identity.BusinessLogic.Validators;

namespace Shelfie.Identity.BusinessLogic.UseCases.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<string?>>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterUserCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<OperationResult<string?>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<string?>();
           
        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists != null)
        {
            result.ErrorCode = ErrorCode.EntityAlreadyExists;
            return result;
        }

        IdentityUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username
        };

        var identityResult = await _userManager.CreateAsync(user, request.Password);

        result = new OperationResult<string?>
        {
            Success = identityResult.Succeeded,
            Result = user.Id,
        };

        return result;
    }
}

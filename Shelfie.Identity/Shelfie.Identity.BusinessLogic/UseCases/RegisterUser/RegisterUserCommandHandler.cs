using MediatR;
using Microsoft.AspNetCore.Identity;
using Shelfie.Identity.BusinessLogic.Enums;
using Shelfie.Identity.BusinessLogic.Models;
using Shelfie.Identity.BusinessLogic.Validators;

namespace Shelfie.Identity.BusinessLogic.UseCases.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, OperationResult<string?>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RegisterUserCommandValidator _validator;

    public RegisterUserCommandHandler(UserManager<IdentityUser> userManager, RegisterUserCommandValidator validator)
    {
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<OperationResult<string?>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return OperationResult<string?>.ValidationFailure(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
        }

        var userExists = await _userManager.FindByNameAsync(request.Username);
        if (userExists != null)
        {
            return OperationResult<string?>.Failure(ErrorCode.EntityAlreadyExists);
        }

        IdentityUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username
        };

        var identityResult = await _userManager.CreateAsync(user, request.Password);

        return new OperationResult<string?>
        {
            IsSuccess = identityResult.Succeeded,
            Data = user.Id,
        };
    }
}

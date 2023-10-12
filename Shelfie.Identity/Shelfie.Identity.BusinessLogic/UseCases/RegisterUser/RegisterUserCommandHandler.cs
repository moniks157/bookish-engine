using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Shelfie.Identity.BusinessLogic.UseCases.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IdentityResult>
{
    private readonly UserManager<IdentityUser> _userManager;

    public RegisterUserCommandHandler(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _userManager.FindByNameAsync(request.Username);

        if (userExists != null)
            return null!;

        IdentityUser user = new()
        {
            Email = request.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = request.Username
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return result;
    }
}

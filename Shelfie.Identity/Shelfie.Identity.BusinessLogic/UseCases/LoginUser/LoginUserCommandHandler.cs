using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shelfie.Identity.BusinessLogic.Enums;
using Shelfie.Identity.BusinessLogic.Models;
using Shelfie.Identity.BusinessLogic.Services.Interfaces;
using Shelfie.Identity.BusinessLogic.Validators;

namespace Shelfie.Identity.BusinessLogic.UseCases.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, OperationResult<string?>>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;
    private readonly LoginUserCommandValidator _validator;

    public LoginUserCommandHandler(UserManager<IdentityUser> userManager, IJwtService jwtService, LoginUserCommandValidator validator)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _validator = validator;
    }

    public async Task<OperationResult<string?>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = new OperationResult<string?>();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            result.ErrorCode = ErrorCode.InvalidRequest;
            result.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            return result;
        }

        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return result;
        }

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = _jwtService.GetToken(authClaims);

        result = new OperationResult<string?>
        {
            Success = true,
            Result = new JwtSecurityTokenHandler().WriteToken(token),
        };

        return result;
    }
}

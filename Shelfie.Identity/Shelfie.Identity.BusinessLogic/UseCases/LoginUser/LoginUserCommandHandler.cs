using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shelfie.Identity.BusinessLogic.Services.Interfaces;

namespace Shelfie.Identity.BusinessLogic.UseCases.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string?>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(UserManager<IdentityUser> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<string?> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            var token = _jwtService.GetToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        return null;
    }
}

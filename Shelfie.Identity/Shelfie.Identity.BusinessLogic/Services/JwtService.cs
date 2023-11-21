using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shelfie.Identity.BusinessLogic.Options;
using Shelfie.Identity.BusinessLogic.Services.Interfaces;

namespace Shelfie.Identity.BusinessLogic.Services;

public class JwtService : IJwtService
{
    private readonly JwtOptions _options;

    public JwtService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public JwtSecurityToken GetToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Secret!));

        var token = new JwtSecurityToken(
            issuer: _options.ValidIssuer,
            audience: _options.ValidAudience,
            expires: DateTime.Now.AddHours(_options.TokenExpiration!),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}

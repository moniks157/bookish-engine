using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shelfie.Identity.BusinessLogic.Services.Interfaces;

public interface IJwtService
{
    JwtSecurityToken GetToken(List<Claim> claims);
}

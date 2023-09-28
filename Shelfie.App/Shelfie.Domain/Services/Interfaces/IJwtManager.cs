using Shelfie.Repository.Entities;

namespace Shelfie.Domain.Services.Interfaces;

public interface IJwtManager
{
    string GenerateToken(User user);
}

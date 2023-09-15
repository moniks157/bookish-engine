using MediatR;

namespace Shelfie.Domain.Services;

public interface IPasswordHasher
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    byte[] GenerateSalt();
}

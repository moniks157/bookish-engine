namespace Shelfie.Domain.Services;

public class PasswordHasher : IPasswordHasher
{
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public byte[] GenerateSalt()
    {
        throw new NotImplementedException();
    }
}

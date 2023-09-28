using FluentAssertions;
using NUnit.Framework;
using Shelfie.Domain.Services;

namespace Shaelfie.Domain.Test.Services;

public class PaswordHasherTests
{
    [Test]
    public void VerifyPasswordHash_ReturnsTrue_WhenPasswordCorrect()
    {
        var password = "password";
        var passwordHasher = new PasswordHasher();

        passwordHasher.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
        
        var result = passwordHasher.VerifyPasswordHash(password, passwordHash, passwordSalt);
        
        result.Should().BeTrue();
    }

    [Test]
    public void VerifyPasswordHash_ReturnsFalse_WhenPasswordIncorrect()
    {
        var password = "password";
        var incorrectPassword = "incorrectPassword";
        var passwordHasher = new PasswordHasher();

        passwordHasher.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        var result = passwordHasher.VerifyPasswordHash(incorrectPassword, passwordHash, passwordSalt);

        result.Should().BeFalse();
    }
}

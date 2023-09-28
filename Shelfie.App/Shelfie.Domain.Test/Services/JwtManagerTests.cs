using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Shelfie.Domain.Services;
using Shelfie.Domain.Test.Fakers;

namespace Shaelfie.Domain.Test.Services;

public class JwtManagerTests
{
    [Test]
    public void GenerateJwtToken_ReturnsToken_WhenUserIsValid()
    {
        var user = new UserFaker().Generate();
        var configuration = new Mock<IConfiguration>();
        var jwtManager = new JwtManager(configuration.Object);

        configuration.Setup(configuration => configuration.GetSection("JWT:Key").Value)
            .Returns("cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e");

        var result = jwtManager.GenerateToken(user);

        result.Should().NotBeNullOrEmpty();
    }
}

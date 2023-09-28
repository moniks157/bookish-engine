using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shelfie.Domain.Services.Interfaces;
using Shelfie.Domain.Test.Fakers;
using Shelfie.Domain.UseCases.LoginUser;
using Shelfie.Repository.Entities;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Domain.Commands;

public class LoginUserCommandHandlerTests
{
    [Test]
    public void Handle_ReturnsNull_WhenUserDoesNotExist()
    {
        var command = new LoginUserCommandFaker().Generate();
        var userRepository = new Mock<IUserRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var jasonWebTokenManager = new Mock<IJwtManager>();

        userRepository.Setup(x => x.ExistsUser(command.Username)).Returns(false);
        
        var handler = new LoginUserCommandHandler(userRepository.Object, passwordHasher.Object, jasonWebTokenManager.Object);
        
        var result = handler.Handle(command, CancellationToken.None).Result;
        
        result.Should().BeNull();
    }

    [Test]
    public void Handle_ReturnsNull_WhenPasswordIsIncorrect()
    {
        var command = new LoginUserCommandFaker().Generate();
        var userRepository = new Mock<IUserRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var jasonWebTokenManager = new Mock<IJwtManager>();

        userRepository.Setup(x => x.ExistsUser(command.Username)).Returns(true);
        userRepository.Setup(x => x.GetUser(command.Username)).Returns(new User());
        passwordHasher.Setup(x => x.VerifyPasswordHash(command.Password, It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(false);
        
        var handler = new LoginUserCommandHandler(userRepository.Object, passwordHasher.Object, jasonWebTokenManager.Object);
        
        var result = handler.Handle(command, CancellationToken.None).Result;
        
        result.Should().BeNull();
    }

    [Test]
    public void Handle_ReturnsToken_WhenPasswordIsCorrect()
    {
        var command = new LoginUserCommandFaker().Generate();
        var userRepository = new Mock<IUserRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var jasonWebTokenManager = new Mock<IJwtManager>();

        userRepository.Setup(x => x.ExistsUser(command.Username)).Returns(true);
        userRepository.Setup(x => x.GetUser(command.Username)).Returns(new User());
        passwordHasher.Setup(x => x.VerifyPasswordHash(command.Password, It.IsAny<byte[]>(), It.IsAny<byte[]>())).Returns(true);
        jasonWebTokenManager.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("token");
        
        var handler = new LoginUserCommandHandler(userRepository.Object, passwordHasher.Object, jasonWebTokenManager.Object);
        
        var result = handler.Handle(command, CancellationToken.None).Result;
        
        result.Should().NotBeNull();
    }
}

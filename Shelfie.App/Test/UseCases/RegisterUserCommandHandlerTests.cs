using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shelfie.Domain.Services.Interfaces;
using Shelfie.Domain.Test.Fakers;
using Shelfie.Domain.UseCases.RegisterUser;
using Shelfie.Repository.Entities;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Domain.Commands;

public class RegisterUserCommandHandlerTests
{
    [Test]
    public void Handle_ReturnsNull_WhenUserAlreadyExists()
    {
        var command = new RegisterUserCommandFaker().Generate();

        var userRepository = new Mock<IUserRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();

        userRepository.Setup(x => x.ExistsUser(command.Username)).Returns(true);

        var handler = new RegisterUserCommandHandler(userRepository.Object, passwordHasher.Object);

        var result = handler.Handle(command, CancellationToken.None).Result;

        result.Should().BeNull();
    }

    [Test]
    public void Handle_ReturnsUser_WhenUserDoesNotExist()
    {
        var command = new RegisterUserCommandFaker().Generate();

        var userRepository = new Mock<IUserRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        
        userRepository.Setup(x => x.ExistsUser(command.Username)).Returns(false);
        userRepository.Setup(x => x.CreateUser(It.IsAny<User>())).Returns(new User());
        
        var handler = new RegisterUserCommandHandler(userRepository.Object, passwordHasher.Object);
        
        var result = handler.Handle(command, CancellationToken.None).Result;
        
        result.Should().NotBeNull();
    }
}

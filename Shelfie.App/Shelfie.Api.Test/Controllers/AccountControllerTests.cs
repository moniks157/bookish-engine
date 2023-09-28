using AutoMapper;
using Bogus;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shelfie.Api.Controllers;
using Shelfie.Api.Models;
using Shelfie.Api.Test.Fakers;
using Shelfie.Domain.UseCases.LoginUser;
using Shelfie.Domain.UseCases.RegisterUser;
using Shelfie.Repository.Entities;

namespace Shelfie.Api.Test.Controllers;

public class AccountControllerTests
{
    [Test]
    public async Task Register_ReturnsOk_WhenUserCreated()
    {
        var registerUserModel = new RegisterUserModelFaker().Generate();
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var accountController = new AccountController(mediator.Object, mapper.Object);

        mapper.Setup(m => m.Map<RegisterUserCommand>(It.IsAny<RegisterUserModel>())).Returns(new RegisterUserCommand());
        mediator.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new User() { Username = registerUserModel.Username }));

        var result = await accountController.Register(registerUserModel);

        result.Should().BeOfType<OkResult>();
    }

    [Test]
    public async Task Register_ReturnsBadRequest_WhenUserAlreadyExists()
    {
        var registerUserModel = new RegisterUserModelFaker().Generate();
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var accountController = new AccountController(mediator.Object, mapper.Object);

        mapper.Setup(m => m.Map<RegisterUserCommand>(It.IsAny<RegisterUserModel>())).Returns(new RegisterUserCommand());
        mediator.Setup(m => m.Send(It.IsAny<RegisterUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<User>(null!));

        var result = await accountController.Register(registerUserModel);

        result.Should().BeOfType<BadRequestObjectResult>();
    }

    [Test]
    public async Task Login_ReturnsOk_WhenUserIsValid()
    {
        var loginUserModel = new LoginUserModelFaker().Generate();
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var accountController = new AccountController(mediator.Object, mapper.Object);

        mapper.Setup(m => m.Map<LoginUserCommand>(It.IsAny<LoginUserModel>())).Returns(new LoginUserCommand());
        mediator.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult("token"));

        var result = await accountController.Login(loginUserModel);

        result.Should().BeOfType<OkObjectResult>();
    }

    [Test]
    public async Task Login_ReturnsBadRequest_WhenUserIsInvalid()
    {
        var loginUserModel = new LoginUserModelFaker().Generate();
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var accountController = new AccountController(mediator.Object, mapper.Object);

        mapper.Setup(m => m.Map<LoginUserCommand>(It.IsAny<LoginUserModel>())).Returns(new LoginUserCommand());
        mediator.Setup(m => m.Send(It.IsAny<LoginUserCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<string>(null!));

        var result = await accountController.Login(loginUserModel);

        result.Should().BeOfType<BadRequestObjectResult>();
    }
}

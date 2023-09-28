using AutoMapper;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Shelfie.Api.Controllers;
using Shelfie.Api.Models;
using Shelfie.Api.Test.Fakers;
using Shelfie.Domain.UseCases.Books.CreateBook;
using Shelfie.Domain.UseCases.Books.GetBook;
using Shelfie.Repository.Entities;

namespace Shelfie.Api.Test.Controllers;

public class BookControllerTests
{
    [Test]
    public async Task Post_ReturnsBook_WhenBookCreated()
    {
        var createBookModel = new CreateBookModelFaker().Generate();
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var bookController = new BookController(mediator.Object, mapper.Object);

        mapper.Setup(m => m.Map<CreateBookCommand>(It.IsAny<CreateBookModel>())).Returns(new CreateBookCommand());
        mediator.Setup(m => m.Send(It.IsAny<CreateBookCommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(new Book() { Title = createBookModel.Title }));

        var result = await bookController.Post(createBookModel);

        result.Should().BeOfType<CreatedAtActionResult>();
    }

    [Test]
    public async Task Get_ReturnsBook_WhenBookExists()
    {
        var id = 1;
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var bookController = new BookController(mediator.Object, mapper.Object);

        mapper.Setup(m => m.Map<GetBookQuery>(It.IsAny<int>())).Returns(new GetBookQuery());
        mediator.Setup(m => m.Send(It.IsAny<GetBookQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Book?>(new Book()));

        var result = await bookController.Get(id);
        result.Should().BeOfType<OkObjectResult>();
    }

    [Test]
    public async Task Get_ReturnsNotFound_WhenBookNotExists()
    {
        var id = 1;
        var mediator = new Mock<IMediator>();
        var mapper = new Mock<IMapper>();
        var bookController = new BookController(mediator.Object, mapper.Object);

        mapper.Setup(m => m.Map<GetBookQuery>(It.IsAny<int>())).Returns(new GetBookQuery());
        mediator.Setup(m => m.Send(It.IsAny<GetBookQuery>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult<Book?>(null));

        var result = await bookController.Get(id);
        result.Should().BeOfType<NotFoundResult>();
    }
}

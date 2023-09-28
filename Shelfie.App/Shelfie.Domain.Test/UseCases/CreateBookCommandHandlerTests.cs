using FluentAssertions;
using Moq;
using NUnit.Framework;
using Shaelfie.Domain.Test.Fakers;
using Shelfie.Domain.UseCases.Books.CreateBook;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shaelfie.Domain.Test.UseCases;

public class CreateBookCommandHandlerTests
{
    [Test]
    public async Task Handle_ReturnsCreatedBook_WhenCalled()
    {
        var command = new CreateBookCommandFaker().Generate();
        var mockBookRepository = new Mock<IBookRepository>();

        var handler = new CreateBookCommandHandler(mockBookRepository.Object);
        
        var result = await handler.Handle(command, CancellationToken.None);
        
        result.Title.Should().Be(command.Title);
    }
}

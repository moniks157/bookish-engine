using MediatR;
using Shelfie.Repository.Entities;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Domain.UseCases.Books.CreateBook;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Book>
{
    private readonly IBookRepository _bookRepository;

    public CreateBookCommandHandler(IBookRepository bookRepository) 
    {
        _bookRepository = bookRepository;
    }

    public Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Title = request.Title
        };

        _bookRepository.AddBook(book);

        return Task.FromResult(book);
    }
}

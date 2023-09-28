using MediatR;
using Shelfie.Repository.Entities;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Domain.UseCases.Books.GetBook;

public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book?>
{
    private readonly IBookRepository _bookRepository;

    public GetBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<Book?> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_bookRepository.GetBook(request.Id));
    }
}

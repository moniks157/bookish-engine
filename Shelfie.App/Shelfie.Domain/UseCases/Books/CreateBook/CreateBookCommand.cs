using MediatR;
using Shelfie.Repository.Entities;

namespace Shelfie.Domain.UseCases.Books.CreateBook;

public class CreateBookCommand : IRequest<Book>
{
    public string Title { get; set; }
}

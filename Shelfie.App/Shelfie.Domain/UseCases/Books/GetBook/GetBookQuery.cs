using MediatR;
using Shelfie.Repository.Entities;

namespace Shelfie.Domain.UseCases.Books.GetBook;

public class GetBookQuery : IRequest<Book?>
{
    public int Id { get; set; }
}

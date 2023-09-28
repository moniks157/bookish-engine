using Shelfie.Repository.Entities;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Repository.Repositories;

public class BookRepository : IBookRepository
{
    private readonly ShelfieDbContext _context;

    public BookRepository(ShelfieDbContext context)
    {
        _context = context;
    }

    public void AddBook(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();
    }

    public Book? GetBook(int id)
    {
        var book = _context.Books.FirstOrDefault(x => x.Id == id);
        return book;
    }
}

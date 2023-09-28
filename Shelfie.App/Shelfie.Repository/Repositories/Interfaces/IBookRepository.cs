using Shelfie.Repository.Entities;

namespace Shelfie.Repository.Repositories.Interfaces;

public interface IBookRepository
{
    void AddBook(Book book);
    Book? GetBook(int id);
}

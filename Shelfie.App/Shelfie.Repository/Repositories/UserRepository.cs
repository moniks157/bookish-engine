using Shelfie.Repository.Models;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Repository.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ShelfieDbContext _context;

    public UserRepository(ShelfieDbContext context)
    {
        _context = context;
    }

    public User? GetUser(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public User? GetUser(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public User? CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }
}

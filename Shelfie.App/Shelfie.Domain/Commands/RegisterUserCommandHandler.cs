using MediatR;
using Shelfie.Domain.Services;
using Shelfie.Repository.Models;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Domain.Commands;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return null;
        }

        //validate if user exists

        _passwordHasher.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var user = new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };

        return _userRepository.CreateUser(user);
    }
}

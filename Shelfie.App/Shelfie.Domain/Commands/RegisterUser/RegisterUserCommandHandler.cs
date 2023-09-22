using EnsureThat;
using MediatR;
using Shelfie.Domain.Services.Interfaces;
using Shelfie.Repository.Entities;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Domain.Commands.RegisterUser;

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
        Ensure.That(request).IsNotNull();

        if (_userRepository.ExistsUser(request.Username))
        {
            return null;
        }

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

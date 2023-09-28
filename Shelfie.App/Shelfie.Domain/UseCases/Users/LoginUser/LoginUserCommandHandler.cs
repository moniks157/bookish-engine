using MediatR;
using Shelfie.Domain.Services.Interfaces;
using Shelfie.Repository.Repositories.Interfaces;

namespace Shelfie.Domain.UseCases.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtManager _jasonWebTokenManager;

        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtManager jasonWebTokenManager)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jasonWebTokenManager = jasonWebTokenManager;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUser(request.Username);

            if (user == null) { return null!; }

            if (_passwordHasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return _jasonWebTokenManager.GenerateToken(user);
            }

            return null!;
        }
    }
}

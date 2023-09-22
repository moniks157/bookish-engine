using MediatR;
using Shelfie.Repository.Entities;

namespace Shelfie.Domain.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<User>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

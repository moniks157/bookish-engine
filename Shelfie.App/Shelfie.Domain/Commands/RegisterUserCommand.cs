using MediatR;
using Shelfie.Repository.Models;

namespace Shelfie.Domain.Commands;

public record RegisterUserCommand(
    string Username, 
    string Password)
    : IRequest<User>;

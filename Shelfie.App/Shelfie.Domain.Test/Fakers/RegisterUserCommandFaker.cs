using Bogus;
using Shelfie.Domain.UseCases.RegisterUser;

namespace Shelfie.Domain.Test.Fakers;

public class RegisterUserCommandFaker : Faker<RegisterUserCommand>
{
    public RegisterUserCommandFaker() 
    {
        RuleFor(c => c.Username, f => f.Internet.UserName());
        RuleFor(c => c.Password, f => f.Internet.Password());
    }
}

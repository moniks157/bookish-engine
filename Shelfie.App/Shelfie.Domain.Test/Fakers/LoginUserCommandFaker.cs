using Bogus;
using Shelfie.Domain.UseCases.LoginUser;

namespace Shelfie.Domain.Test.Fakers;

public class LoginUserCommandFaker : Faker<LoginUserCommand>
{
    public LoginUserCommandFaker() 
    {
        RuleFor(c => c.Username, f => f.Internet.UserName());
        RuleFor(c => c.Password, f => f.Internet.Password());
    }
}

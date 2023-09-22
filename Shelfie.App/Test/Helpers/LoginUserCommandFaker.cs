using Bogus;
using Shelfie.Domain.Commands.LoginUser;

namespace Test.Helpers
{
    public class LoginUserCommandFaker : Faker<LoginUserCommand>
    {
        public LoginUserCommandFaker() 
        {
            RuleFor(c => c.Username, f => f.Internet.UserName());
            RuleFor(c => c.Password, f => f.Internet.Password());
        }
    }
}

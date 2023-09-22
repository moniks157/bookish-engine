using Bogus;
using Shelfie.Domain.Commands.RegisterUser;

namespace Test.Helpers
{
    public class RegisterUserCommandFaker : Faker<RegisterUserCommand>
    {
        public RegisterUserCommandFaker() 
        {
            RuleFor(c => c.Username, f => f.Internet.UserName());
            RuleFor(c => c.Password, f => f.Internet.Password());
        }
    }
}

using Bogus;
using Shelfie.Api.Models;

namespace Shelfie.Api.Test.Fakers;

public class RegisterUserModelFaker : Faker<RegisterUserModel>
{
    public RegisterUserModelFaker()
    {
        RuleFor(c => c.Username, f => f.Internet.UserName());
        RuleFor(c => c.Password, f => f.Internet.Password());
    }
}

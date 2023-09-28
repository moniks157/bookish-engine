using Bogus;
using Shelfie.Api.Models;

namespace Shelfie.Api.Test.Fakers;

public class LoginUserModelFaker : Faker<LoginUserModel>
{
    public LoginUserModelFaker()
    {
        RuleFor(c => c.Username, f => f.Internet.UserName());
        RuleFor(c => c.Password, f => f.Internet.Password());
    }
}

using Bogus;
using Shelfie.Repository.Entities;

namespace Shelfie.Domain.Test.Fakers;

public class UserFaker : Faker<User>
{
    public UserFaker()
    {
        RuleFor(u => u.Id, f => f.Random.Int());
        RuleFor(u => u.Username, f => f.Internet.UserName());
        RuleFor(u => u.PasswordHash, f => f.Random.Bytes(64));
        RuleFor(u => u.PasswordSalt, f => f.Random.Bytes(128));
    }
}

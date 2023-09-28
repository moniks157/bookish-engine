using Bogus;
using Shelfie.Api.Models;

namespace Shelfie.Api.Test.Fakers;

public class CreateBookModelFaker : Faker<CreateBookModel>
{
    public CreateBookModelFaker() 
    {
        RuleFor(x => x.Title, f => f.Lorem.Sentence());
    }
}

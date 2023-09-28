using Bogus;
using Shelfie.Domain.UseCases.Books.CreateBook;

namespace Shaelfie.Domain.Test.Fakers;

public class CreateBookCommandFaker: Faker<CreateBookCommand>
{
    public CreateBookCommandFaker() 
    {
        RuleFor(x => x.Title, f => f.Lorem.Sentence());
    }
}

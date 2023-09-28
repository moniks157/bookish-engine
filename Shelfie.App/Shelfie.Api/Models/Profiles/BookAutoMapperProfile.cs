using AutoMapper;
using Shelfie.Domain.UseCases.Books.CreateBook;
using Shelfie.Domain.UseCases.Books.GetBook;

namespace Shelfie.Api.Models.Profiles;

public class BookAutoMapperProfile : Profile
{
    public BookAutoMapperProfile()
    {
        CreateMap<CreateBookModel, CreateBookCommand>();
    }
}

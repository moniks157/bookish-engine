using AutoMapper;
using Shelfie.Domain.UseCases.LoginUser;
using Shelfie.Domain.UseCases.RegisterUser;

namespace Shelfie.Api.Models.Profiles
{
    public class UserAutoMapperProfile : Profile
    {
        public UserAutoMapperProfile()
        {
            CreateMap<RegisterUserModel, RegisterUserCommand>();
            CreateMap<LoginUserModel, LoginUserCommand>();
        }
    }
}

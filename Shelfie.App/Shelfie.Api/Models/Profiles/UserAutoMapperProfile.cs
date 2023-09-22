using AutoMapper;
using Shelfie.Domain.Commands.LoginUser;
using Shelfie.Domain.Commands.RegisterUser;

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

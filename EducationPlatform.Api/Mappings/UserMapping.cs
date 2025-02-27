using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.UserDto;

namespace EducationPlatform.Api.Mappings
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<User, ResultUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
        }
        
    }
}

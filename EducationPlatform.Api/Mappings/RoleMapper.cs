using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.RoleDto;

namespace EducationPlatform.Api.Mappings
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<CreateRoleDto, Role>().ReverseMap();
            CreateMap<Role, ResultRoleDto>().ReverseMap();
        }
    }
}

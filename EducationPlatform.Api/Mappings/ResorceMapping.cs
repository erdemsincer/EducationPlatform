using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;

namespace EducationPlatform.Api.Mappings
{
    public class ResourceMapping : Profile
    {
        public ResourceMapping()
        {
            CreateMap<Resource, ResultResourceDto>().ReverseMap();
            CreateMap<Resource, UpdateResourceDto>().ReverseMap();
            CreateMap<Resource, CreateResourceDto>().ReverseMap();
        }

    }
}

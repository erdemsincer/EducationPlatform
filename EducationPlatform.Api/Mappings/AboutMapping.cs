using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.AboutDto;
using EducationPlatform.Dto.MessageDto;

namespace EducationPlatform.Api.Mappings
{
    public class AboutMapping : Profile
    {
        public AboutMapping()
        {
            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
        }
    }
}

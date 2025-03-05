using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using EducationPlatform.Dto.SocialMediaDto;

namespace EducationPlatform.Api.Mappings
{
    public class SocialMediaMapping : Profile
    {
        public SocialMediaMapping()
        {
            CreateMap<SocialMedia, ResultSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, UpdateSocialMediaDto>().ReverseMap();
            CreateMap<SocialMedia, CreateSocialMediaDto>().ReverseMap();
        }
    }
}

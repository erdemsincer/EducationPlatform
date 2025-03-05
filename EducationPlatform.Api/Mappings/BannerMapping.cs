using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.BannerDto;

namespace EducationPlatform.Api.Mappings
{
    public class BannerMapping : Profile
    {
        public BannerMapping()
        {
            CreateMap<Banner, ResultBannerDto>().ReverseMap();
            CreateMap<Banner, CreateBannerDto>().ReverseMap();
            CreateMap<Banner, UpdateBannerDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.InterestDto;

namespace EducationPlatform.Api.Mappings
{
    public class InterestMapping:Profile
    {
        public InterestMapping()
        {
            CreateMap<Interest, ResultInterestDto>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName)).ReverseMap();
            CreateMap<Interest, CreateInterestDto>().ReverseMap();
            CreateMap<Interest, UpdateInterestDto>().ReverseMap();
        }
    }
}

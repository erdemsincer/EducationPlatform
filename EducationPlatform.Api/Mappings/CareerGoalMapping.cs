using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CareerGoalDto;

namespace EducationPlatform.Api.Mappings
{
    public class CareerGoalMapping:Profile
    {
        public CareerGoalMapping()
        {
            CreateMap<CareerGoal, ResultCareerGoalDto>()
                 .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName)) // User'dan FullName al
                 .ReverseMap();
            CreateMap<CareerGoal, CreateCareerGoalDto>().ReverseMap();
            CreateMap<CareerGoal, UpdateCareerGoalDto>().ReverseMap();
        }
    }
}

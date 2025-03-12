using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.SkillDto;

namespace EducationPlatform.Api.Mappings
{
    public class SkillMapping : Profile
    {
        public SkillMapping()
        {
            // Skill'den ResultSkillDto'ya eşleme, UserName'i User'dan al
            CreateMap<Skill, ResultSkillDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName)) // UserName'i User'dan al
                .ReverseMap(); // ReverseMap, her iki yönlü eşlemeyi sağlar

            // Skill'den CreateSkillDto'ya eşleme
            CreateMap<Skill, CreateSkillDto>().ReverseMap();

            // Skill'den UpdateSkillDto'ya eşleme
            CreateMap<Skill, UpdateSkillDto>().ReverseMap();
        }
    }
}

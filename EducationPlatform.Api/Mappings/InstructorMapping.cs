using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.InstructorDto;
using EducationPlatform.Dto.ReviewDto;

namespace EducationPlatform.Api.Mappings
{
    public class InstructorMapping:Profile
    {
        public InstructorMapping()
        {
            CreateMap<Instructor, CreateInstructorDto>().ReverseMap();
            CreateMap<Instructor, UpdateInstructorDto>().ReverseMap();
            CreateMap<Instructor, ResultInstructorDto>().ReverseMap();
            CreateMap<Instructor, InstructorWithReviewsDto>()
              .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
              .ReverseMap();
        }
    }
}

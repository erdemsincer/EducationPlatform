using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.TestimonialDto;
using EducationPlatform.Dto.UserDto;

namespace EducationPlatform.Api.Mappings
{
    public class TestimonialMapping:Profile
    {
        public TestimonialMapping()
        {
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
        }
    }
}

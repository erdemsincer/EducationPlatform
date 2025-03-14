using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ReviewDto;

namespace EducationPlatform.Api.Mappings
{
    public class ReviewMapping : Profile
    {
        public ReviewMapping()
        {
            CreateMap<Review, ResultReviewDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName)) // Kullanıcı adını getir
                .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName)) // Eğitmen adını getir
                .ReverseMap();

            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
        }
    }
}

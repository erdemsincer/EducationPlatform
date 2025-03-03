using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.DiscussionDto;

namespace EducationPlatform.Api.Mappings
{
    public class DiscussionMapper : Profile
    {
        public DiscussionMapper()
        {
            CreateMap<Discussion, ResultDiscussionDto>().ReverseMap();
            CreateMap<Discussion, CreateDiscussionDto>().ReverseMap();
            CreateMap<Discussion, UpdateDiscussionDto>().ReverseMap();

        }
    }
}

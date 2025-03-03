using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.DiscussionDto;
using EducationPlatform.Dto.DiscussionReplyDto;

namespace EducationPlatform.Api.Mappings
{
    public class DiscussionReplyMapping : Profile
    {
        public DiscussionReplyMapping()
        {
            CreateMap<DiscussionReply, ResultDiscussionReplyDto>().ReverseMap();
            CreateMap<DiscussionReply, CreateDiscussionReplyDto>().ReverseMap();
        }
    }
}

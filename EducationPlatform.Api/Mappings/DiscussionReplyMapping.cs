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
            CreateMap<DiscussionReply, ResultDiscussionReplyDto>()
    .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName));

            CreateMap<Discussion, DiscussionWithRepliesDto>()
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies));

        }
    }
}

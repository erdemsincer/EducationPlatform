using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CommentDto;

namespace EducationPlatform.Api.Mappings
{
    public class CommentMapping : Profile
    {
        public CommentMapping()
        {
            CreateMap<Comment, ResultCommentDto>().ReverseMap();
            CreateMap<Comment, UpdateCommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();
        }

    }
}

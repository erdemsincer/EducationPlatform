using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ContactDto;
using EducationPlatform.Dto.MessageDto;

namespace EducationPlatform.Api.Mappings
{
    public class MessageMapping : Profile
    {
        public MessageMapping()
        {
            CreateMap<Message, ResultMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
        }
    }
}

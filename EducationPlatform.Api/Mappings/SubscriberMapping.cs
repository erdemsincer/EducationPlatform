using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.SubscriberDto;
using EducationPlatform.Dto.UserDto;

namespace EducationPlatform.Api.Mappings
{
    public class SubscriberMapping : Profile
    {
        public SubscriberMapping()
        {

            CreateMap<Subscriber, ResultSubscriberDto>().ReverseMap();
            CreateMap<Subscriber, UpdateSubscriberDto>().ReverseMap();
            CreateMap<Subscriber, CreateSubscriberDto>().ReverseMap();
        }
    }
}

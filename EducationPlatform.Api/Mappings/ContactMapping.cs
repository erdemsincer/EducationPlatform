using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CategoryDto;
using EducationPlatform.Dto.ContactDto;

namespace EducationPlatform.Api.Mappings
{
    public class ContactMapping : Profile
    {
        public ContactMapping()
        {
            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
        }
    }
}

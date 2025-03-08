using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;

namespace EducationPlatform.Api.Mappings
{
    public class ResourceMapping : Profile
    {
        public ResourceMapping()
        {
            CreateMap<Resource, ResultResourceDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName)) // Kullanıcı adı eşleme
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); // Kategori adı eşleme
            CreateMap<Resource, UpdateResourceDto>().ReverseMap();
            CreateMap<Resource, CreateResourceDto>().ReverseMap();
        }

    }
}

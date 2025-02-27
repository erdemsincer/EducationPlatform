using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CategoryDto;

namespace EducationPlatform.Api.Mappings
{
    public class CategoryMapping : Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }

    }
}

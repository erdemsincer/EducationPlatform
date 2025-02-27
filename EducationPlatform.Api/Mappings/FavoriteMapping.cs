using AutoMapper;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.FavoriteDto;

namespace EducationPlatform.Api.Mappings
{
    public class FavoriteMapping : Profile
    {
        public FavoriteMapping()
        {
            CreateMap<Favorite, ResultFavoriteDto>().ReverseMap();
            CreateMap<Favorite, UpdateFavoriteDto>().ReverseMap();
            CreateMap<Favorite, CreateFavoriteDto>().ReverseMap();
        }

    }
}

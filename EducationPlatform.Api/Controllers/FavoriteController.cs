using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.FavoriteDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        private readonly IMapper _mapper;

        public FavoriteController(IFavoriteService favoriteService, IMapper mapper)
        {
            _favoriteService = favoriteService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult FavoriteList()
        {
            var values = _favoriteService.TGetListAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateFavorite(CreateFavoriteDto createFavoriteDto)
        {
            var values = _mapper.Map<Favorite>(createFavoriteDto);
            _favoriteService.TAdd(values);
            return Ok("eklendi");
        }
        [HttpPut]
        public IActionResult UpdateFavorite(UpdateFavoriteDto updateFavoriteDto)
        {
            var values = _mapper.Map<Favorite>(updateFavoriteDto);

            _favoriteService.TUpdate(values);
            return Ok("Güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteFavorite(int id)
        {
            var value = _favoriteService.TGetById(id);
            _favoriteService.TDelete(value);
            return Ok("Silindi");
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdFavorite(int id)
        {
            var value = _favoriteService.TGetById(id);
            return Ok(value);
        }
        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId(int id)
        {
            var value = _favoriteService.GetByUserId(id);
            return Ok(value);
        }
        
    }
}

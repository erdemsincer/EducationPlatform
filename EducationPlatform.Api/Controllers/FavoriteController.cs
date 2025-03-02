using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.FavoriteDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> FavoriteList()
        {
            var values = await _favoriteService.TGetListAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFavorite(CreateFavoriteDto createFavoriteDto)
        {
            var values = _mapper.Map<Favorite>(createFavoriteDto);
            await _favoriteService.TAddAsync(values);
            return Ok("Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFavorite(UpdateFavoriteDto updateFavoriteDto)
        {
            var values = _mapper.Map<Favorite>(updateFavoriteDto);
            await _favoriteService.TUpdateAsync(values);
            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            var value = await _favoriteService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Favori bulunamadı");
            }

            await _favoriteService.TDeleteAsync(value);
            return Ok("Silindi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdFavorite(int id)
        {
            var value = await _favoriteService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Favori bulunamadı");
            }

            return Ok(value);
        }

        [HttpGet("GetByUserId")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var value = await _favoriteService.GetByUserIdAsync(id);
            if (value == null || value.Count == 0)
            {
                return NotFound("Bu kullanıcıya ait favori bulunamadı");
            }

            return Ok(value);
        }
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetFavoritesByUserId(int userId)
        {
            var favorites = await _favoriteService.GetFavoritesByUserIdAsync(userId);

            var result = _mapper.Map<List<ResultFavoriteDto>>(favorites);

            return Ok(result);
        }

    }
}

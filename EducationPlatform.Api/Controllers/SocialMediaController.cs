using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.SocialMediaDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocialMediaController : ControllerBase
    {
        private readonly ISocialMediaService _socialmediaService;
        private readonly IMapper _mapper;

        public SocialMediaController(ISocialMediaService socialmediaService, IMapper mapper)
        {
            _socialmediaService = socialmediaService;
            _mapper = mapper;
        }

        // 🔹 Tüm SocialMedia verilerini getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _socialmediaService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultSocialMediaDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir SocialMedia getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _socialmediaService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            var result = _mapper.Map<ResultSocialMediaDto>(value);
            return Ok(result);
        }

        // 🔹 SocialMedia ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateSocialMediaDto dto)
        {
            var socialmedia = _mapper.Map<SocialMedia>(dto);
            await _socialmediaService.TAddAsync(socialmedia);
            return Ok("Hakkımızda bilgisi eklendi.");
        }

        // 🔹 SocialMedia güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateSocialMediaDto dto)
        {
            var socialmedia = _mapper.Map<SocialMedia>(dto);
            await _socialmediaService.TUpdateAsync(socialmedia);
            return Ok("Hakkımızda bilgisi güncellendi.");
        }

        // 🔹 SocialMedia sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _socialmediaService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            await _socialmediaService.TDeleteAsync(value);
            return Ok("Hakkımızda bilgisi silindi.");
        }
    }
}

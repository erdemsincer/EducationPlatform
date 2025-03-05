using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.BannerDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        private readonly IMapper _mapper;

        public BannerController(IBannerService bannerService, IMapper mapper)
        {
            _bannerService = bannerService;
            _mapper = mapper;
        }

        // 🔹 Tüm Banner verilerini getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _bannerService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultBannerDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir Banner getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _bannerService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            var result = _mapper.Map<ResultBannerDto>(value);
            return Ok(result);
        }

        // 🔹 Banner ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateBannerDto dto)
        {
            var banner = _mapper.Map<Banner>(dto);
            await _bannerService.TAddAsync(banner);
            return Ok("Hakkımızda bilgisi eklendi.");
        }

        // 🔹 Banner güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateBannerDto dto)
        {
            var banner = _mapper.Map<Banner>(dto);
            await _bannerService.TUpdateAsync(banner);
            return Ok("Hakkımızda bilgisi güncellendi.");
        }

        // 🔹 Banner sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _bannerService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            await _bannerService.TDeleteAsync(value);
            return Ok("Hakkımızda bilgisi silindi.");
        }
    }
}

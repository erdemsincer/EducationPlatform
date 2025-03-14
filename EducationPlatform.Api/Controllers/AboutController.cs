    using AutoMapper;
    using EducationPlatform.Application.Abstract;
    using EducationPlatform.Domain.Entities;
    using EducationPlatform.Dto.AboutDto;
    using Microsoft.AspNetCore.Mvc;

    namespace EducationPlatform.Api.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class AboutController : ControllerBase
        {
            private readonly IAboutService _aboutService;
            private readonly IMapper _mapper;

            public AboutController(IAboutService aboutService, IMapper mapper)
            {
                _aboutService = aboutService;
                _mapper = mapper;
            }

            // 🔹 Tüm About verilerini getir
            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var values = await _aboutService.TGetListAllAsync();
                var result = _mapper.Map<List<ResultAboutDto>>(values);
                return Ok(result);
            }

            // 🔹 Tek bir About getir (ID ile)
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
            {
                var value = await _aboutService.TGetByIdAsync(id);
                if (value == null)
                    return NotFound("Hakkımızda bilgisi bulunamadı.");

                var result = _mapper.Map<ResultAboutDto>(value);
                return Ok(result);
            }

            // 🔹 About ekle
            [HttpPost]
            public async Task<IActionResult> Create(CreateAboutDto dto)
            {
                var about = _mapper.Map<About>(dto);
                await _aboutService.TAddAsync(about);
                return Ok("Hakkımızda bilgisi eklendi.");
            }

            // 🔹 About güncelle
            [HttpPut]
            public async Task<IActionResult> Update(UpdateAboutDto dto)
            {
                var about = _mapper.Map<About>(dto);
                await _aboutService.TUpdateAsync(about);
                return Ok("Hakkımızda bilgisi güncellendi.");
            }

            // 🔹 About sil
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var value = await _aboutService.TGetByIdAsync(id);
                if (value == null)
                    return NotFound("Hakkımızda bilgisi bulunamadı.");

                await _aboutService.TDeleteAsync(value);
                return Ok("Hakkımızda bilgisi silindi.");
            }
        }
    }

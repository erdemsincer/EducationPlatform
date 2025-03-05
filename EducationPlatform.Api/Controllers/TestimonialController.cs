using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.TestimonialDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonaiService _testimonialService;
        private readonly IMapper _mapper;

        public TestimonialController(ITestimonaiService testimonialService, IMapper mapper)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
        }

        // 🔹 Tüm Testimonial verilerini getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _testimonialService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultTestimonialDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir Testimonial getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _testimonialService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            var result = _mapper.Map<ResultTestimonialDto>(value);
            return Ok(result);
        }

        // 🔹 Testimonial ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateTestimonialDto dto)
        {
            var testimonial = _mapper.Map<Testimonial>(dto);
            await _testimonialService.TAddAsync(testimonial);
            return Ok("Hakkımızda bilgisi eklendi.");
        }

        // 🔹 Testimonial güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateTestimonialDto dto)
        {
            var testimonial = _mapper.Map<Testimonial>(dto);
            await _testimonialService.TUpdateAsync(testimonial);
            return Ok("Hakkımızda bilgisi güncellendi.");
        }

        // 🔹 Testimonial sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _testimonialService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            await _testimonialService.TDeleteAsync(value);
            return Ok("Hakkımızda bilgisi silindi.");
        }
    }
}

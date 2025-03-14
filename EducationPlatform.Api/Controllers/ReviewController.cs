using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.InstructorDto;
using EducationPlatform.Dto.ReviewDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IMapper mapper)
        {
            _reviewService = reviewService;
            _mapper = mapper;
        }

        // 🔹 Tüm değerlendirmeleri getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reviews = await _reviewService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultReviewDto>>(reviews);
            return Ok(result);
        }

        // 🔹 Belirli bir değerlendirmeyi getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _reviewService.TGetByIdAsync(id);
            if (review == null)
                return NotFound("Değerlendirme bulunamadı.");

            var result = _mapper.Map<ResultReviewDto>(review);
            return Ok(result);
        }

        // 🔹 Belirli bir eğitmenin tüm değerlendirmelerini getir
        [HttpGet("by-instructor/{instructorId}")]
        public async Task<IActionResult> GetByInstructorId(int instructorId)
        {
            var reviews = await _reviewService.GetReviewsByInstructorIdAsync(instructorId);
            var result = _mapper.Map<List<ResultReviewDto>>(reviews);
            return Ok(result);
        }

        // 🔹 Yeni değerlendirme ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateReviewDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            await _reviewService.TAddAsync(review);
            return Ok("Değerlendirme başarıyla eklendi.");
        }

        // 🔹 Değerlendirme güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateReviewDto dto)
        {
            var review = _mapper.Map<Review>(dto);
            await _reviewService.TUpdateAsync(review);
            return Ok("Değerlendirme başarıyla güncellendi.");
        }

        // 🔹 Değerlendirme sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.TGetByIdAsync(id);
            if (review == null)
                return NotFound("Değerlendirme bulunamadı.");

            await _reviewService.TDeleteAsync(review);
            return Ok("Değerlendirme başarıyla silindi.");
        }

     


    }
}

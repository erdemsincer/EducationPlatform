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
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly IMapper _mapper;

        public InstructorController(IInstructorService instructorService, IMapper mapper)
        {
            _instructorService = instructorService;
            _mapper = mapper;
        }

        // 🔹 Tüm eğitmenleri getir (DEĞERLENDİRMELERİYLE BİRLİKTE)
      

        // 🔹 Tüm eğitmenleri getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instructors = await _instructorService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultInstructorDto>>(instructors);
            return Ok(result);
        }

        // 🔹 Belirli bir eğitmeni getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var instructor = await _instructorService.TGetByIdAsync(id);
            if (instructor == null)
                return NotFound("Eğitmen bulunamadı.");

            var result = _mapper.Map<ResultInstructorDto>(instructor);
            return Ok(result);
        }

        // 🔹 Yeni eğitmen ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorDto dto)
        {
            var instructor = _mapper.Map<Instructor>(dto);
            await _instructorService.TAddAsync(instructor);
            return Ok("Eğitmen başarıyla eklendi.");
        }

        // 🔹 Eğitmen güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInstructorDto dto)
        {
            var instructor = _mapper.Map<Instructor>(dto);
            await _instructorService.TUpdateAsync(instructor);
            return Ok("Eğitmen bilgileri güncellendi.");
        }

        // 🔹 Eğitmeni sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _instructorService.TGetByIdAsync(id);
            if (instructor == null)
                return NotFound("Eğitmen bulunamadı.");

            await _instructorService.TDeleteAsync(instructor);
            return Ok("Eğitmen başarıyla silindi.");
        }
        [HttpGet("details/{instructorId}")]
        public async Task<IActionResult> GetInstructorDetails(int instructorId)
        {
            var instructor = await _instructorService.GetInstructorWithReviewsAsync(instructorId);

            if (instructor == null)
                return NotFound("Eğitmen bulunamadı.");

            // Eğitmene ait yorumları kontrol et
            instructor.Reviews ??= new List<Review>();

            // Ortalama Puan Hesaplama
            double averageRating = instructor.Reviews.Any() ? instructor.Reviews.Average(r => r.Rating) : 0;

            // DTO'ya Dönüştürme
            var result = _mapper.Map<InstructorWithReviewsDto>(instructor);
            result.AverageRating = averageRating;
            result.Reviews ??= new List<ResultReviewDto>(); // Eğer `null` ise boş bir liste ata.

            return Ok(result);
        }

        [HttpGet("last-four")]
        public async Task<IActionResult> GetLastFourInstructors()
        {
            var instructors = await _instructorService.GetLastFourInstructorsAsync();

            if (instructors == null || !instructors.Any())
                return NotFound("Hiç eğitmen bulunamadı.");

            var result = _mapper.Map<List<ResultInstructorDto>>(instructors);
            return Ok(result);
        }

    }
}

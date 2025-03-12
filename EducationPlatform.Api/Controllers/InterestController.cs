using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.InterestDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly IInterestService _interestService;
        private readonly IMapper _mapper;

        public InterestController(IInterestService interestService, IMapper mapper)
        {
            _interestService = interestService;
            _mapper = mapper;
        }

        // 🔹 Kullanıcıya ait ilgi alanlarını getir
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var interests = await _interestService.GetInterestsByUserAsync(userId);
            if (interests == null || !interests.Any())
            {
                return NotFound("İlgi alanı bulunamadı.");
            }

            var result = _mapper.Map<List<ResultInterestDto>>(interests); // Dönüştürme işlemi
            return Ok(result);
        }

        // 🔹 Tüm ilgi alanlarını getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _interestService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultInterestDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir ilgi alanını getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _interestService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("İlgi alanı bulunamadı.");

            var result = _mapper.Map<ResultInterestDto>(value);
            return Ok(result);
        }

        // 🔹 İlgi alanı ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateInterestDto dto)
        {
            var interest = _mapper.Map<Interest>(dto);
            await _interestService.TAddAsync(interest);
            return Ok("İlgi alanı eklendi.");
        }

        // 🔹 İlgi alanı güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateInterestDto dto)
        {
            var interest = _mapper.Map<Interest>(dto);
            await _interestService.TUpdateAsync(interest);
            return Ok("İlgi alanı güncellendi.");
        }

        // 🔹 İlgi alanı sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _interestService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("İlgi alanı bulunamadı.");

            await _interestService.TDeleteAsync(value);
            return Ok("İlgi alanı silindi.");
        }
    }
}

using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.SkillDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;

        public SkillController(ISkillService skillService, IMapper mapper)
        {
            _skillService = skillService;
            _mapper = mapper;
        }

        // 🔹 Kullanıcıya ait becerileri getir
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetSkillsByUser(int userId)
        {
            var skills = await _skillService.GetSkillsByUserAsync(userId);
            if (skills == null || !skills.Any())
            {
                return NotFound("Beceri bilgisi bulunamadı.");
            }

            var result = _mapper.Map<List<ResultSkillDto>>(skills);
            return Ok(result);
        }

        // 🔹 Tüm becerileri getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _skillService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultSkillDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir beceri getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _skillService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Beceri bilgisi bulunamadı.");

            var result = _mapper.Map<ResultSkillDto>(value);
            return Ok(result);
        }

        // 🔹 Beceri ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateSkillDto dto)
        {
            var skill = _mapper.Map<Skill>(dto);
            await _skillService.TAddAsync(skill);
            return Ok("Beceri bilgisi eklendi.");
        }

        // 🔹 Beceri güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateSkillDto dto)
        {
            var skill = _mapper.Map<Skill>(dto);
            await _skillService.TUpdateAsync(skill);
            return Ok("Beceri bilgisi güncellendi.");
        }

        // 🔹 Beceri sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _skillService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Beceri bilgisi bulunamadı.");

            await _skillService.TDeleteAsync(value);
            return Ok("Beceri bilgisi silindi.");
        }
    }

}

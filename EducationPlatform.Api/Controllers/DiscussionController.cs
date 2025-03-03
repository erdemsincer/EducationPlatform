using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.DiscussionDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class DiscussionController : ControllerBase
    {
        private readonly IDiscussionService _discussionService;
        private readonly IMapper _mapper;

        public DiscussionController(IDiscussionService discussionService, IMapper mapper)
        {
            _discussionService = discussionService;
            _mapper = mapper;
        }

        // ✅ Tüm Tartışmaları Listele
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var discussions = await _discussionService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultDiscussionDto>>(discussions);
            return Ok(result);
        }

        // ✅ ID'ye Göre Tartışma Getir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discussion = await _discussionService.TGetByIdAsync(id);
            if (discussion == null)
                return NotFound("Tartışma bulunamadı.");

            var result = _mapper.Map<ResultDiscussionDto>(discussion);
            return Ok(result);
        }

        // ✅ Yeni Tartışma Oluştur
        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscussionDto dto)
        {
            var discussion = _mapper.Map<Discussion>(dto);
            await _discussionService.TAddAsync(discussion);
            return Ok("Tartışma başarıyla oluşturuldu.");
        }

        // ✅ Tartışmayı Güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDiscussionDto dto)
        {
            var discussion = _mapper.Map<Discussion>(dto);
            await _discussionService.TUpdateAsync(discussion);
            return Ok("Tartışma başarıyla güncellendi.");
        }

        // ✅ Tartışmayı Sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var discussion = await _discussionService.TGetByIdAsync(id);
            if (discussion == null)
                return NotFound("Tartışma bulunamadı.");

            await _discussionService.TDeleteAsync(discussion);
            return Ok("Tartışma başarıyla silindi.");
        }
    }
}

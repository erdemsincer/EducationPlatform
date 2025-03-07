using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.DiscussionDto;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var discussions = await _discussionService.GetDiscussionsWithUserAndReplyCountAsync();
            var result = _mapper.Map<List<ResultDiscussionDto>>(discussions);
            return Ok(result);
        }

       
        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var discussions = await _discussionService.GetDiscussionsByUserIdAsync(userId);
            if (discussions == null || !discussions.Any())
                return NotFound("Bu kullanıcıya ait tartışma bulunamadı.");

            var result = _mapper.Map<List<ResultDiscussionDto>>(discussions);
            return Ok(result);
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var discussion = await _discussionService.TGetByIdAsync(id);
            if (discussion == null)
                return NotFound("Tartışma bulunamadı.");

            var result = _mapper.Map<ResultDiscussionDto>(discussion);
            return Ok(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(CreateDiscussionDto dto)
        {
            var discussion = _mapper.Map<Discussion>(dto);
            await _discussionService.TAddAsync(discussion);
            return Ok("Tartışma başarıyla oluşturuldu.");
        }


        [HttpPut]
        public async Task<IActionResult> Update(UpdateDiscussionDto dto)
        {
            if (dto.Id == 0 || dto.UserId == 0)
                return BadRequest("Eksik bilgiler!");

            var discussion = _mapper.Map<Discussion>(dto);
           
            await _discussionService.TUpdateAsync(discussion);

            return Ok("Güncellendi");
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var discussion = await _discussionService.TGetByIdAsync(id);
            if (discussion == null)
                return NotFound("Tartışma bulunamadı.");

            await _discussionService.TDeleteAsync(discussion);
            return Ok("Tartışma başarıyla silindi.");
        }
        [HttpGet("GetLastDiscussions")]
        public async Task<IActionResult> GetLastDiscussions()
        {
            var discussions = await _discussionService.GetLastDiscussionsAsync(4);
            var result = _mapper.Map<List<ResultDiscussionDto>>(discussions);
            return Ok(result);
        }
        [HttpGet("GetDiscussionDetailWithReplies/{id}")]
        public async Task<IActionResult> GetDiscussionDetailWithReplies(int id)
        {
            var discussion = await _discussionService.GetDiscussionWithRepliesByIdAsync(id);

            if (discussion == null)
                return NotFound("Tartışma bulunamadı.");

            var result = _mapper.Map<DiscussionWithRepliesDto>(discussion);

            return Ok(result);
        }



    }
}

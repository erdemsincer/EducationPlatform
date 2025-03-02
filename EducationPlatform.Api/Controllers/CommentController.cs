using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CommentDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CommentList()
        {
            var values = await _commentService.TGetListAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto)
        {
            var values = _mapper.Map<Comment>(createCommentDto);
            await _commentService.TAddAsync(values);
            return Ok("Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var values = _mapper.Map<Comment>(updateCommentDto);
            await _commentService.TUpdateAsync(values);
            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var value = await _commentService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı");
            }

            await _commentService.TDeleteAsync(value);
            return Ok("Silindi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdComment(int id)
        {
            var value = await _commentService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Yorum bulunamadı");
            }

            return Ok(value);
        }

        [HttpGet("GetByResourceId")]
        public async Task<IActionResult> GetByResourceId(int id)
        {
            var value = await _commentService.GetByResourceIdAsync(id);
            if (value == null || value.Count == 0)
            {
                return NotFound("Bu kaynak için yorum bulunamadı");
            }

            return Ok(value);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetCommentsByUserId(int userId)
        {
            var comments = await _commentService.GetCommentsByUserIdAsync(userId);

            var result = _mapper.Map<List<ResultCommentDto>>(comments);

            return Ok(result);
        }

    }
}

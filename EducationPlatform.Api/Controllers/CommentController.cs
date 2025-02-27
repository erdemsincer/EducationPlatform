using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.CommentDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CommentList()
        {
            var values = _commentService.TGetListAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateComment(CreateCommentDto createCommentDto)
        {
            var values = _mapper.Map<Comment>(createCommentDto);
            _commentService.TAdd(values);
            return Ok("eklendi");
        }
        [HttpPut]
        public IActionResult UpdateComment(UpdateCommentDto updateCommentDto)
        {
            var values = _mapper.Map<Comment>(updateCommentDto);

            _commentService.TUpdate(values);
            return Ok("Güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            var value = _commentService.TGetById(id);
            _commentService.TDelete(value);
            return Ok("Silindi");
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdComment(int id)
        {
            var value = _commentService.TGetById(id);
            return Ok(value);
        }
        [HttpGet("GetByResourceId")]
        public IActionResult GetByResourceId(int id)
        {
            var value = _commentService.GetByResourceId(id);
            return Ok(value);
        }
       
        

    }
}

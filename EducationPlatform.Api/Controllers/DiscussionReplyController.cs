﻿using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.DiscussionReplyDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class DiscussionReplyController : ControllerBase
    {
        private readonly IDiscussionReplyService _discussionReplyService;
        private readonly IMapper _mapper;

        public DiscussionReplyController(IDiscussionReplyService discussionReplyService, IMapper mapper)
        {
            _discussionReplyService = discussionReplyService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetReplies()
        {
            var values = await _discussionReplyService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultDiscussionReplyDto>>(values);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReply(int id)
        {
            var value = await _discussionReplyService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Yanıt bulunamadı.");

            var result = _mapper.Map<ResultDiscussionReplyDto>(value);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReply(CreateDiscussionReplyDto dto)
        {
            var reply = _mapper.Map<DiscussionReply>(dto);
            await _discussionReplyService.TAddAsync(reply);
            return Ok("Yanıt başarıyla eklendi.");
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var reply = await _discussionReplyService.TGetByIdAsync(id);
            if (reply == null)
                return NotFound("Yanıt bulunamadı.");

            await _discussionReplyService.TDeleteAsync(reply);
            return Ok("Yanıt başarıyla silindi.");
        }
    }
}

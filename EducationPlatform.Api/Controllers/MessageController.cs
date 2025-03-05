using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.MessageDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        // 🔹 Tüm Message verilerini getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _messageService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultMessageDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir Message getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _messageService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            var result = _mapper.Map<ResultMessageDto>(value);
            return Ok(result);
        }

        // 🔹 Message ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageDto dto)
        {
            var message = _mapper.Map<Message>(dto);
            await _messageService.TAddAsync(message);
            return Ok("Hakkımızda bilgisi eklendi.");
        }

        // 🔹 Message güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateMessageDto dto)
        {
            var message = _mapper.Map<Message>(dto);
            await _messageService.TUpdateAsync(message);
            return Ok("Hakkımızda bilgisi güncellendi.");
        }

        // 🔹 Message sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _messageService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            await _messageService.TDeleteAsync(value);
            return Ok("Hakkımızda bilgisi silindi.");
        }
    }
}

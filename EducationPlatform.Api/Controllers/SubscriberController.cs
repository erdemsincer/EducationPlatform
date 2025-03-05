using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.SubscriberDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;

        public SubscriberController(ISubscriberService subscriberService, IMapper mapper)
        {
            _subscriberService = subscriberService;
            _mapper = mapper;
        }

        // 🔹 Tüm Subscriber verilerini getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _subscriberService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultSubscriberDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir Subscriber getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _subscriberService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            var result = _mapper.Map<ResultSubscriberDto>(value);
            return Ok(result);
        }

        // 🔹 Subscriber ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateSubscriberDto dto)
        {
            var subscriber = _mapper.Map<Subscriber>(dto);
            await _subscriberService.TAddAsync(subscriber);
            return Ok("Hakkımızda bilgisi eklendi.");
        }

        // 🔹 Subscriber güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateSubscriberDto dto)
        {
            var subscriber = _mapper.Map<Subscriber>(dto);
            await _subscriberService.TUpdateAsync(subscriber);
            return Ok("Hakkımızda bilgisi güncellendi.");
        }

        // 🔹 Subscriber sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _subscriberService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            await _subscriberService.TDeleteAsync(value);
            return Ok("Hakkımızda bilgisi silindi.");
        }
    }
}

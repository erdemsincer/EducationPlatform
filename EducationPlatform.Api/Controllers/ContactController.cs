using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ContactDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }

        // 🔹 Tüm Contact verilerini getir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var values = await _contactService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultContactDto>>(values);
            return Ok(result);
        }

        // 🔹 Tek bir Contact getir (ID ile)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var value = await _contactService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            var result = _mapper.Map<ResultContactDto>(value);
            return Ok(result);
        }

        // 🔹 Contact ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateContactDto dto)
        {
            var contact = _mapper.Map<Contact>(dto);
            await _contactService.TAddAsync(contact);
            return Ok("Hakkımızda bilgisi eklendi.");
        }

        // 🔹 Contact güncelle
        [HttpPut]
        public async Task<IActionResult> Update(UpdateContactDto dto)
        {
            var contact = _mapper.Map<Contact>(dto);
            await _contactService.TUpdateAsync(contact);
            return Ok("Hakkımızda bilgisi güncellendi.");
        }

        // 🔹 Contact sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _contactService.TGetByIdAsync(id);
            if (value == null)
                return NotFound("Hakkımızda bilgisi bulunamadı.");

            await _contactService.TDeleteAsync(value);
            return Ok("Hakkımızda bilgisi silindi.");
        }
    }
}

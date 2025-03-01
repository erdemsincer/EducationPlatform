using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;
        private readonly IMapper _mapper;

        public ResourceController(IResourceService resourceService, IMapper mapper)
        {
            _resourceService = resourceService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ResourceList()
        {
            var values = await _resourceService.TGetListAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResource(CreateResourceDto createResourceDto)
        {
            var values = _mapper.Map<Resource>(createResourceDto);
            await _resourceService.TAddAsync(values);
            return Ok("Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateResource([FromBody] UpdateResourceDto updateResourceDto)
        {
            var values = _mapper.Map<Resource>(updateResourceDto);
            await _resourceService.TUpdateAsync(values);
            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var value = await _resourceService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Kaynak bulunamadı");
            }

            await _resourceService.TDeleteAsync(value);
            return Ok("Silindi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdResource(int id)
        {
            var value = await _resourceService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Kaynak bulunamadı");
            }

            return Ok(value);
        }

        [HttpGet("GetByCategoryId")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            var value = await _resourceService.GetByCategoryIdAsync(id);
            if (value == null || value.Count == 0)
            {
                return NotFound("Bu kategoriye ait kaynak bulunamadı");
            }

            return Ok(value);
        }
        [HttpGet("GetResourceDetails")]
        public async Task<ActionResult<List<ResultResourceDto>>> GetResourceDetails()
        {
            var resources = await _resourceService.GetResourceDetailsAsync();
            return Ok(resources);
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetUserResources(int userId)
        {
            var userResources = await _resourceService.GetResourcesByUserIdAsync(userId);

            if (userResources == null || !userResources.Any())
            {
                return NotFound("Kullanıcının eklediği herhangi bir kaynak bulunamadı.");
            }

            return Ok(userResources);
        }
    }
}

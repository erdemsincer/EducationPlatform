using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> UpdateResource(UpdateResourceDto updateResourceDto)
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
        [HttpGet("GetByCategory/{categoryId}")]
        public async Task<ActionResult<List<ResultResourceDto>>> GetResourcesByCategory(int categoryId)
        {
            var resources = await _resourceService.GetResourcesByCategoryWithUser(categoryId);
            if (resources == null || resources.Count == 0)
            {
                return NotFound("Bu kategoriye ait kaynak bulunamadı.");
            }

            // **Burada Mapping İşlemi Yapılıyor**
            var resourceDtos = _mapper.Map<List<ResultResourceDto>>(resources);
            return Ok(resourceDtos);
        }
        [HttpGet("GetLatestResources")]
        public async Task<ActionResult<List<ResultResourceDto>>> GetLatestResources()
        {
            var resources = await _resourceService.GetLatestResources();
            if (resources == null || resources.Count == 0)
            {
                return NotFound("Hiç kaynak bulunamadı.");
            }

            // AutoMapper ile Entity -> DTO dönüşümü
            var resourceDtos = _mapper.Map<List<ResultResourceDto>>(resources);
            return Ok(resourceDtos);
        }
        [HttpGet("GetResourceById/{id}")]
        public async Task<IActionResult> GetResourceById(int id)
        {
            var resourceDto = await _resourceService.GetResourceByIdAsync(id);
            if (resourceDto == null)
            {
                return NotFound("Kaynak bulunamadı");
            }

            return Ok(resourceDto);
        }
    }
}

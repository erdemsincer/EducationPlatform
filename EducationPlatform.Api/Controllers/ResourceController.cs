using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ResourceList()
        {
            var values = _resourceService.TGetListAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateResource(CreateResourceDto createResourceDto)
        {
            var values = _mapper.Map<Resource>(createResourceDto);
            _resourceService.TAdd(values);
            return Ok("eklendi");
        }
        [HttpPut]
        public IActionResult UpdateResource(UpdateResourceDto updateResourceDto)
        {
            var values = _mapper.Map<Resource>(updateResourceDto);

            _resourceService.TUpdate(values);
            return Ok("Güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteResource(int id)
        {
            var value = _resourceService.TGetById(id);
            _resourceService.TDelete(value);
            return Ok("Silindi");
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdResource(int id)
        {
            var value = _resourceService.TGetById(id);
            return Ok(value);
        }
    }
}

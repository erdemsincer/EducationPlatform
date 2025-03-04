using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.RoleDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.TGetListAllAsync();
            var result = _mapper.Map<List<ResultRoleDto>>(roles);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateRoleDto dto)
        {
            var role = _mapper.Map<Role>(dto);
            await _roleService.TAddAsync(role);
            return Ok("Rol eklendi.");
        }
    }
    }

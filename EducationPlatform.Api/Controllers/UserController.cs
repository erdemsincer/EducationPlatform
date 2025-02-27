using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.UserDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult UserList()
        {
            var values = _userService.TGetListAll();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult CreateUser(CreateUserDto createUserDto)
        {
            var values = _mapper.Map<User>(createUserDto);
            _userService.TAdd(values);
            return Ok("eklendi");
        }
        [HttpPut]
        public IActionResult UpdateUser(UpdateUserDto updateUserDto)
        {
            var values = _mapper.Map<User>(updateUserDto);

            _userService.TUpdate(values);
            return Ok("Güncellendi");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var value = _userService.TGetById(id);
            _userService.TDelete(value);
            return Ok("Silindi");
        }
        [HttpGet("{id}")]
        public IActionResult GetByIdUser(int id)
        {
            var value = _userService.TGetById(id);
            return Ok(value);
        }
    }
}

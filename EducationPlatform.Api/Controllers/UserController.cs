﻿using AutoMapper;
using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<IActionResult> UserList()
        {
            var values = await _userService.TGetListAllAsync();
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var values = _mapper.Map<User>(createUserDto);
            await _userService.TAddAsync(values);
            return Ok("Eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var values = _mapper.Map<User>(updateUserDto);
            await _userService.TUpdateAsync(values);
            return Ok("Güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var value = await _userService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            await _userService.TDeleteAsync(value);
            return Ok("Silindi");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdUser(int id)
        {
            var value = await _userService.TGetByIdAsync(id);
            if (value == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            return Ok(value);
        }
    }
}

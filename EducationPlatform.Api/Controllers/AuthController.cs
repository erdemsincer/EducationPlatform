using EducationPlatform.Application.Abstract;
using EducationPlatform.Application.Security;
using EducationPlatform.Dto.AuthDto;
using EducationPlatform.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
         private readonly TokenGenerator _tokenGenerator;

        public AuthController(IUserService userService, TokenGenerator tokenGenerator)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var existingUser = await _userService.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Bu e-posta adresi zaten kullanımda.");
            }

            var hashedPassword = PasswordHelper.HashPassword(registerDto.Password);

            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                ProfileImage = "ERDEM",
                Role = registerDto.Role

            };

            await _userService.TAddAsync(newUser);
            return Ok("Kayıt başarılı.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Kullanıcı bulunamadı.");
            }

            bool isPasswordCorrect = PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                return Unauthorized("Şifre hatalı.");
            }

            var token = _tokenGenerator.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}

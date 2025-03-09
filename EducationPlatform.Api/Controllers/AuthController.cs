using EducationPlatform.Application.Abstract;
using EducationPlatform.Application.Security;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.AuthDto;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenGenerator _tokenGenerator;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;


        public AuthController(IUserService userService, TokenGenerator tokenGenerator, IRoleService roleService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _tokenGenerator = tokenGenerator;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // 1️⃣ Kullanıcının daha önce kayıt olup olmadığını kontrol et
            var existingUser = await _userService.GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Bu e-posta adresi zaten kullanımda.");
            }

            // 2️⃣ Şifreyi hash'le
            var hashedPassword = PasswordHelper.HashPassword(registerDto.Password);

            // 3️⃣ Yeni kullanıcı oluştur
            var newUser = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                ProfileImage = "default.png" // Varsayılan profil resmi
            };

            // 4️⃣ Kullanıcıyı veritabanına ekle
            await _userService.TAddAsync(newUser);

            // 5️⃣ "Student" rolü var mı kontrol et, yoksa oluştur
            var roles = await _roleService.TGetListAllAsync();
            var studentRole = roles.FirstOrDefault(r => r.Name == "Student");

            if (studentRole == null)
            {
                studentRole = new Role { Name = "Student" };
                await _roleService.TAddAsync(studentRole);
            }

            // 6️⃣ Kullanıcıya "Student" rolünü ata
            await _userRoleService.AssignRoleToUserAsync(newUser.Id, studentRole.Id);

            return Ok(new { message = "Kayıt başarılı! Kullanıcıya 'Student' rolü atandı." });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.GetUserWithRolesByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Kullanıcı bulunamadı.");
            }

            bool isPasswordCorrect = PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash);
            if (!isPasswordCorrect)
            {
                return Unauthorized("Şifre hatalı.");
            }

            // ✅ Rolleri çekiyoruz
            var roles = user.UserRoles.Select(ur => ur.Role.Name).ToList();

            // ✅ Token oluşturuyoruz (roller dahil)
            var token = _tokenGenerator.GenerateToken(user, roles);

            return Ok(new
            {
                Token = token,
                 Roles = roles
            });
        }
    }
}

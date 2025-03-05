using EducationPlatform.Application.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        // ✅ Kullanıcıya Rol Ata
        [HttpPost("Assign")]
        public async Task<IActionResult> AssignRole(int userId, int roleId)
        {
            await _userRoleService.AssignRoleAsync(userId, roleId);
            return Ok("Rol başarıyla atandı.");
        }

        // ✅ Kullanıcıdan Rol Sil
        [HttpDelete("Remove")]
        public async Task<IActionResult> RemoveRole(int userId, int roleId)
        {
            await _userRoleService.RemoveRoleAsync(userId, roleId);
            return Ok("Rol başarıyla silindi.");
        }

        // ✅ Kullanıcının Rollerini Getir
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var roles = await _userRoleService.GetUserRolesAsync(userId);
            return Ok(roles);
        }
    }
}

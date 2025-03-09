using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRoleDal _userRoleDal;
        private readonly IRoleDal _roleDal;

        public UserRoleManager(IUserRoleDal userRoleDal, IRoleDal roleDal)
        {
            _userRoleDal = userRoleDal;
            _roleDal = roleDal;
        }

        public async Task AssignRoleAsync(int userId, int roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            await _userRoleDal.AddAsync(userRole);
        }

        public async Task RemoveRoleAsync(int userId, int roleId)
        {
            var userRole = await _userRoleDal.GetAsync(x => x.UserId == userId && x.RoleId == roleId);
            if (userRole != null)
            {
                await _userRoleDal.DeleteAsync(userRole);
            }
        }

        public async Task<List<Role>> GetUserRolesAsync(int userId)
        {
            return await _userRoleDal.GetUserRolesAsync(userId);
        }
        public async Task AssignRoleToUserAsync(int userId, int roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            await _userRoleDal.AddAsync(userRole);
        }
    }
}

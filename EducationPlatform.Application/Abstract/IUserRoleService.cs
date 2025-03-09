using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IUserRoleService 
    {
        Task AssignRoleAsync(int userId, int roleId);
        Task RemoveRoleAsync(int userId, int roleId);
        Task<List<Role>> GetUserRolesAsync(int userId);
        Task AssignRoleToUserAsync(int userId, int roleId);
    }
}

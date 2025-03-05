using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFUserDal : GenericRepository<User>, IUserDal
    {
        private readonly ApplicationDbContext _context;

        public EFUserDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
     .Include(u => u.UserRoles)
         .ThenInclude(ur => ur.Role)
     .FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<User> GetUserWithRolesByEmailAsync(string email)
        {
            return await _context.Users
         .Include(u => u.UserRoles)
             .ThenInclude(ur => ur.Role)
         .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

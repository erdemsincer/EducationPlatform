using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFSkillDal : GenericRepository<Skill>, ISkillDal
    {
        private readonly ApplicationDbContext _context;
        public EFSkillDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Skill>> GetByUserIdAsync(int userId)
        {
            return await _context.Skills
                                .Where(s => s.UserId == userId)
                                .Include(y => y.User)
                                .ToListAsync();
        }
    }
}

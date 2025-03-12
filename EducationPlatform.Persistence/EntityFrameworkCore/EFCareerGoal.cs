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
    public class EFCareerGoal:GenericRepository<CareerGoal>, ICareerGoalDal
    {
        private readonly ApplicationDbContext _context;
        public EFCareerGoal(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<CareerGoal> GetByUserIdAsync(int userId)
        {
            return await _context.CareerGoals
                         .Where(c => c.UserId == userId)
                         .Include(u => u.User)
                         .FirstOrDefaultAsync();

        }
    }
   
}

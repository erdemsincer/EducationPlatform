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
    public class EFInterestDal : GenericRepository<Interest>, IInterestDal
    {
        private readonly ApplicationDbContext _context;
        public EFInterestDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Interest>> GetByUserIdAsync(int userId)
        {
            return await _context.Interests
                                 .Where(i => i.UserId == userId)
                                 .Include(y => y.User)
                                 .ToListAsync();
        }
    }
}

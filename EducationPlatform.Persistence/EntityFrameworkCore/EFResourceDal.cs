using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFResourceDal : GenericRepository<Resource>, IResourceDal
    {
        private readonly ApplicationDbContext _context;

        public EFResourceDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Resource>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Resources
                .Where(r => r.CategoryId == categoryId)
                .ToListAsync();
        }
    }
}

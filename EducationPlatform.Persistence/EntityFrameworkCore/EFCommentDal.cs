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
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly ApplicationDbContext _context;

        public EFCommentDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetByResourceIdAsync(int resourceId)
        {
            return await _context.Comments
                .Where(c => c.ResourceId == resourceId)
                .ToListAsync();
        }
        public async Task<List<Comment>> GetCommentsByUserIdAsync(int userId)
        {
            return await _context.Comments
                .Include(x => x.Resource)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

    }
}

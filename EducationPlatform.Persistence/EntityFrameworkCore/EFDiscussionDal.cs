using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFDiscussionDal : GenericRepository<Discussion>, IDiscussionDal
    {
        private readonly ApplicationDbContext _context;

        public EFDiscussionDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Discussion>> GetDiscussionsWithUserAsync()
        {
            return await _context.Discussions
                .Include(d => d.User)
                .ToListAsync();
        }
        public async Task<List<Discussion>> GetDiscussionsByUserIdAsync(int userId)
        {
            return await _context.Discussions
                .Where(d => d.UserId == userId)
                .Include(d => d.User)
                .ToListAsync();
        }
        public async Task<List<Discussion>> GetLastDiscussionsAsync(int count)
        {
            return await _context.Discussions
                .OrderByDescending(d => d.CreatedAt)
                .Take(count)
                .Include(d => d.User)
                .ToListAsync();
        }


    }
}

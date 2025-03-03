using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFDiscussionReplyDal : GenericRepository<DiscussionReply>, IDiscussionReplyDal
    {
        private readonly ApplicationDbContext _context;

        public EFDiscussionReplyDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DiscussionReply>> GetRepliesWithUserAsync(int discussionId)
        {
            return await _context.DiscussionReplies
                .Where(dr => dr.DiscussionId == discussionId)
                .Include(dr => dr.User)
                .ToListAsync();
        }
    }
}

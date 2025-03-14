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
    public class EFReviewDal : GenericRepository<Review>, IReviewDal
    {
        private readonly ApplicationDbContext _context;
        public EFReviewDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetReviewsByInstructorIdAsync(int instructorId)
        {
            return await _context.Reviews
                .Where(r => r.InstructorId == instructorId)
                .Include(r => r.User)  // Yorumu yazan kullanıcı bilgilerini getir
                .Include(r => r.Instructor)  // Yorumu yapılan eğitmen bilgilerini getir
                .ToListAsync();
        }

    }
}

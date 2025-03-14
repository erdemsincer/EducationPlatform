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
    public class EFInstructorDal : GenericRepository<Instructor>, IInstructorDal
    {
       private readonly ApplicationDbContext _context;

        public EFInstructorDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }



        public async Task<Instructor> GetInstructorWithReviewsAsync(int instructorId)
        {
            return await _context.Instructors
                .Include(i => i.Reviews)  // Eğitmene ait yorumları getir
                .ThenInclude(r => r.User) // Yorumu yazan kullanıcı bilgilerini getir
                .FirstOrDefaultAsync(i => i.Id == instructorId);
        }

        

        public async Task<List<Instructor>> GetLastFourInstructorsAsync()
        {
            return await _context.Instructors
                .OrderByDescending(i => i.Id) // Son eklenenleri almak için ID’ye göre sıralama
                .Take(4) // Sadece son 4 eğitmeni getir
                .ToListAsync();
        }
    }
}

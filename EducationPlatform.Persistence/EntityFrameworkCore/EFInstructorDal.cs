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

      

        public async Task<List<Instructor>> GetInstructorsWithReviewsAsync()
        {
            return await _context.Set<Instructor>()
                .Include(i => i.Reviews)
                .ToListAsync();
        }
    }
}

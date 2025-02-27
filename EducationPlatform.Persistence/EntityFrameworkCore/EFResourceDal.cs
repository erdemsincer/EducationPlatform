using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFResourceDal : GenericRepository<Resource>, IResourceDal
    {
        private readonly ApplicationDbContext _context;

        public EFResourceDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        

        public List<Resource> GetByCategoryId(int categoryId)
        {
            return _context.Resources.Where(r => r.CategoryId == categoryId).ToList();
        }
    }
}

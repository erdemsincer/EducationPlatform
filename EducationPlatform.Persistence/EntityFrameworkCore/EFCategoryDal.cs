using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        private readonly ApplicationDbContext _context;

        public EFCategoryDal(ApplicationDbContext context) : base(context)
        {
            _context=context;
        }

        public Category GetByName(string name)
        {
            return _context.Categories.FirstOrDefault(c => c.Name == name);
        }
    }
}

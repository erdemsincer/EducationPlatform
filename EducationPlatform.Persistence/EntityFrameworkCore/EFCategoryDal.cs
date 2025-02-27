using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFCategoryDal : GenericRepository<Category>, ICategoryDal
    {
        public EFCategoryDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}

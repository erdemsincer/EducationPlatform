using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFResourceDal : GenericRepository<Resource>, IResourceDal
    {
        public EFResourceDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}

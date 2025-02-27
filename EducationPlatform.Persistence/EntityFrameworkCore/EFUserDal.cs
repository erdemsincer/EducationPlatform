using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFUserDal : GenericRepository<User>, IUserDal
    {
        public EFUserDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}

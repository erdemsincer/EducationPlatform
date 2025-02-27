using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFFavoriteDal : GenericRepository<Favorite>, IFavoriteDal
    {
        public EFFavoriteDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}

using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFFavoriteDal : GenericRepository<Favorite>, IFavoriteDal
    {
        private readonly ApplicationDbContext _context;

        public EFFavoriteDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Favorite> GetByUserId(int userId)
        {
            return _context.Favorites.Where(f => f.UserId == userId).ToList();
        }
    }
}

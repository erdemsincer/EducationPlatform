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
    public class EFFavoriteDal : GenericRepository<Favorite>, IFavoriteDal
    {
        private readonly ApplicationDbContext _context;

        public EFFavoriteDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Favorite>> GetByUserIdAsync(int userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }
        // EFFavoriteDal.cs
        public async Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId)
        {
            return await _context.Favorites
                                 .Include(f => f.Resource)
                                 .Where(f => f.UserId == userId)
                                 .ToListAsync();
        }

    }
}

using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFResourceDal : GenericRepository<Resource>, IResourceDal
    {
        private readonly ApplicationDbContext _context;

        public EFResourceDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Resource>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Resources
                .Where(r => r.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<List<ResultResourceDto>> GetResourceDetailsAsync()
        {
            return await _context.Resources
                 .Include(r => r.User)  // Kullanıcı bilgilerini çekiyoruz
                 .Include(r => r.Category)  // Kategori bilgilerini çekiyoruz
                 .Select(r => new ResultResourceDto
                 {
                     Id = r.Id,
                     Title = r.Title,
                     Description = r.Description,
                     FileUrl = r.FileUrl,
                     CategoryId = r.CategoryId,
                     UserId = r.UserId,
                     CreatedAt = r.CreatedAt,
                     UserName = r.User.FullName,  // Kullanıcı Adı
                     CategoryName = r.Category.Name  // Kategori Adı
                 }).ToListAsync();
        }

        public async Task<List<Resource>> GetResourcesByUserIdAsync(int userId)
        {
            return await _context.Resources
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
    }
}

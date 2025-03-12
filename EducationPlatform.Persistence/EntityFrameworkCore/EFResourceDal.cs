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
                 .Include(r => r.User)  
                 .Include(r => r.Category)  
                 .Select(r => new ResultResourceDto
                 {
                     Id = r.Id,
                     Title = r.Title,
                     Description = r.Description,
                     FileUrl = r.FileUrl,
                     CategoryId = r.CategoryId,
                     UserId = r.UserId,
                     CreatedAt = r.CreatedAt,
                     UserName = r.User.FullName,  
                     CategoryName = r.Category.Name  
                 }).ToListAsync();
        }

        public async Task<List<Resource>> GetResourcesByUserIdAsync(int userId)
        {
            return await _context.Resources
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
        public async Task<List<Resource>> GetResourcesByCategoryWithUser(int categoryId)
        {
            return await _context.Resources
               .Where(r => r.CategoryId == categoryId)
               .Include(r => r.User) // Kullanıcıyı dahil et
               .Include(r => r.Category) // Kategori bilgilerini dahil et
               .ToListAsync();
        }
        public async Task<List<Resource>> GetLatestResources()
        {
            return await _context.Resources
                .OrderByDescending(r => r.CreatedAt) // En yeni resource'ları sırala
                .Take(3) // Son 4 tanesini al
                .Include(r => r.User) // Kullanıcı bilgisi ekle
                .Include(r => r.Category) // Kategori bilgisi ekle
                .ToListAsync();
        }

        public async Task<ResultResourceDto> GetResourceByIdAsync(int id)
        {
            return await _context.Resources
                 .Where(r => r.Id == id) // ID ile filtreleme
                 .Include(r => r.User)
                 .Include(r => r.Category)
                 .Select(r => new ResultResourceDto
                 {
                     Id = r.Id,
                     Title = r.Title,
                     Description = r.Description,
                     FileUrl = r.FileUrl,
                     CategoryId = r.CategoryId,
                     UserId = r.UserId,
                     CreatedAt = r.CreatedAt,
                     UserName = r.User.FullName,
                     CategoryName = r.Category.Name
                 })
                 .FirstOrDefaultAsync(); // İlk (veya varsayılan) sonucu döndür
        }
    }
}

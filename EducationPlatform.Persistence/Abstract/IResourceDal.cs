using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IResourceDal : IGenericDal<Resource>
    {
        Task<List<Resource>> GetByCategoryIdAsync(int categoryId);
        Task<List<ResultResourceDto>> GetResourceDetailsAsync();
        Task<List<Resource>> GetResourcesByUserIdAsync(int userId);
        Task<List<Resource>> GetResourcesByCategoryWithUser(int categoryId);
        Task<List<Resource>> GetLatestResources();
        Task<ResultResourceDto> GetResourceByIdAsync(int id);
    }
}

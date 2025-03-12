using EducationPlatform.Domain.Entities;
using EducationPlatform.Dto.ResourceDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IResourceService : IGenericService<Resource>
    {
        Task<List<Resource>> GetByCategoryIdAsync(int categoryId);
        Task<List<ResultResourceDto>> GetResourceDetailsAsync();
        Task<List<ResultResourceDto>> GetResourcesByUserIdAsync(int userId);
        Task<List<Resource>> GetResourcesByCategoryWithUser(int categoryId);
        Task<List<Resource>> GetLatestResources();
        Task<ResultResourceDto> GetResourceByIdAsync(int id);

    }
}

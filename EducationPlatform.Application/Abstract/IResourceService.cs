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
    }
}

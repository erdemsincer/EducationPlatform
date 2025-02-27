using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IResourceService : IGenericService<Resource>
    {
        Task<List<Resource>> GetByCategoryIdAsync(int categoryId);
    }
}

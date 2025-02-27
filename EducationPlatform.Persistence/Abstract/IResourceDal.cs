using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IResourceDal : IGenericDal<Resource>
    {
        Task<List<Resource>> GetByCategoryIdAsync(int categoryId);
    }
}

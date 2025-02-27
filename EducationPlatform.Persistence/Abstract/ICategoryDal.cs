using EducationPlatform.Domain.Entities;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface ICategoryDal : IGenericDal<Category>
    {
        Task<Category> GetByNameAsync(string name);
    }
}

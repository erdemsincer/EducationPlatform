using EducationPlatform.Domain.Entities;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<Category> GetByNameAsync(string name);
    }
}

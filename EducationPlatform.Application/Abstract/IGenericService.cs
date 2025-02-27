using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IGenericService<T> where T : class
    {
        Task TAddAsync(T entity);
        Task TUpdateAsync(T entity);
        Task TDeleteAsync(T entity);
        Task<T> TGetByIdAsync(int id);
        Task<List<T>> TGetListAllAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IGenericDal<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetListAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> filter); // ✅ Burayı ekle
    }
}

using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _categoryDal.GetByNameAsync(name);
        }

        public async Task TAddAsync(Category entity)
        {
            await _categoryDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Category entity)
        {
            await _categoryDal.DeleteAsync(entity);
        }

        public async Task<Category> TGetByIdAsync(int id)
        {
            return await _categoryDal.GetByIdAsync(id);
        }

        public async Task<List<Category>> TGetListAllAsync()
        {
            return await _categoryDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Category entity)
        {
            await _categoryDal.UpdateAsync(entity);
        }
    }
}

using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class AboutManager : IAboutService
    {
        private readonly IAboutDal _aboutDal;

        public AboutManager(IAboutDal aboutDal)
        {
            _aboutDal = aboutDal;
        }

        public async Task TAddAsync(About entity)
        {
            await _aboutDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(About entity)
        {
            await _aboutDal.DeleteAsync(entity);
        }

        public async Task<About> TGetByIdAsync(int id)
        {
            return await _aboutDal.GetByIdAsync(id);
        }

        public async Task<List<About>> TGetListAllAsync()
        {
            return await _aboutDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(About entity)
        {
            await _aboutDal.UpdateAsync(entity);
        }
    }
}

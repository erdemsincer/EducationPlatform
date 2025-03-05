using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class BannerManager : IBannerService
    {
        private readonly IBannerDal _bannerDal;

        public BannerManager(IBannerDal bannerDal)
        {
            _bannerDal = bannerDal;
        }

        public async Task TAddAsync(Banner entity)
        {
            await _bannerDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Banner entity)
        {
            await _bannerDal.DeleteAsync(entity);
        }

        public async Task<Banner> TGetByIdAsync(int id)
        {
            return await _bannerDal.GetByIdAsync(id);
        }

        public async Task<List<Banner>> TGetListAllAsync()
        {
            return await _bannerDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Banner entity)
        {
            await _bannerDal.UpdateAsync(entity);
        }
    }
}

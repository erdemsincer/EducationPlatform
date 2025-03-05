using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class SocialMediaManager : ISocialMediaService
    {
        private readonly ISocialMediaDal _socialMediaDal;

        public SocialMediaManager(ISocialMediaDal socialMediaDal)
        {
            _socialMediaDal = socialMediaDal;
        }

        public async Task TAddAsync(SocialMedia entity)
        {
            await _socialMediaDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(SocialMedia entity)
        {
            await _socialMediaDal.DeleteAsync(entity);
        }

        public async Task<SocialMedia> TGetByIdAsync(int id)
        {
            return await _socialMediaDal.GetByIdAsync(id);
        }

        public async Task<List<SocialMedia>> TGetListAllAsync()
        {
            return await _socialMediaDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(SocialMedia entity)
        {
            await _socialMediaDal.UpdateAsync(entity);
        }
    }
}

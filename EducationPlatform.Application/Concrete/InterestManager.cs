using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

namespace EducationPlatform.Application.Concrete
{
    public class InterestManager : IInterestService
    {
        private readonly IInterestDal _interestDal;

        public InterestManager(IInterestDal interestDal)
        {
            _interestDal = interestDal;
        }

        // Kullanıcının ilgi alanlarını almak
        public async Task<List<Interest>> GetInterestsByUserAsync(int userId)
        {
            return await _interestDal.GetByUserIdAsync(userId);
        }

        // İlgi alanı eklemek
        public async Task TAddAsync(Interest entity)
        {
            await _interestDal.AddAsync(entity);
        }

        // İlgi alanı silmek
        public async Task TDeleteAsync(Interest entity)
        {
            await _interestDal.DeleteAsync(entity);
        }

        // İlgi alanını ID ile almak
        public async Task<Interest> TGetByIdAsync(int id)
        {
            return await _interestDal.GetByIdAsync(id);
        }

        // Tüm ilgi alanlarını almak
        public async Task<List<Interest>> TGetListAllAsync()
        {
            return await _interestDal.GetListAllAsync();
        }

        // İlgi alanını güncellemek
        public async Task TUpdateAsync(Interest entity)
        {
            await _interestDal.UpdateAsync(entity);
            
        }
    }
}

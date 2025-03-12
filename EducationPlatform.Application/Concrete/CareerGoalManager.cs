using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

namespace EducationPlatform.Application.Concrete
{
    public class CareerGoalManager : ICareerGoalService
    {
        private readonly ICareerGoalDal _careerGoalDal;

        public CareerGoalManager(ICareerGoalDal careerGoalDal)
        {
            _careerGoalDal = careerGoalDal;
        }

        // Kullanıcının kariyer hedefini almak
        public async Task<CareerGoal> GetCareerGoalByUserIdAsync(int userId)
        {
            return await _careerGoalDal.GetByUserIdAsync(userId);
        }

        // Kariyer hedefini eklemek
        public async Task TAddAsync(CareerGoal entity)
        {
            await _careerGoalDal.AddAsync(entity);
        }

        // Kariyer hedefini silmek
        public async Task TDeleteAsync(CareerGoal entity)
        {
            await _careerGoalDal.DeleteAsync(entity);
        }

        // Kariyer hedefini ID ile almak
        public async Task<CareerGoal> TGetByIdAsync(int id)
        {
            return await _careerGoalDal.GetByIdAsync(id);
        }

        // Tüm kariyer hedeflerini almak
        public async Task<List<CareerGoal>> TGetListAllAsync()
        {
            return await _careerGoalDal.GetListAllAsync();
        }

        // Kariyer hedefini güncellemek
        public async Task TUpdateAsync(CareerGoal entity)
        {
          await  _careerGoalDal.UpdateAsync(entity);
            
        }
    }
}

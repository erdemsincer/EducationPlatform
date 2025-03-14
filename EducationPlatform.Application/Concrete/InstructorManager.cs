using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.EntityFrameworkCore;

namespace EducationPlatform.Application.Concrete
{
    public class InstructorManager : IInstructorService
    {
        private readonly IInstructorDal _instructorDal;

        public InstructorManager(IInstructorDal instructorDal)
        {
            _instructorDal = instructorDal;
        }

        public async Task<Instructor> GetInstructorWithReviewsAsync(int instructorId)
        {
            return await _instructorDal.GetInstructorWithReviewsAsync(instructorId);
        }

        public async Task TAddAsync(Instructor entity)
        {
            await _instructorDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Instructor entity)
        {
            await _instructorDal.DeleteAsync(entity);
        }

        public async Task<Instructor> TGetByIdAsync(int id)
        {
            return await _instructorDal.GetByIdAsync(id);
        }

        public async Task<List<Instructor>> TGetListAllAsync()
        {
            return await _instructorDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Instructor entity)
        {
            await _instructorDal.UpdateAsync(entity);
        }
        public async Task<List<Instructor>> GetLastFourInstructorsAsync()
        {
            return await _instructorDal.GetLastFourInstructorsAsync();
        }
    }
}

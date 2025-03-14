using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

namespace EducationPlatform.Application.Concrete
{
    public class ReviewManager : IReviewService
    {
        private readonly IReviewDal _reviewDal;

        public ReviewManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }

        public async Task<List<Review>> GetReviewsByInstructorIdAsync(int instructorId)
        {
            return await _reviewDal.GetReviewsByInstructorIdAsync(instructorId);
        }


        public async Task TAddAsync(Review entity)
        {
            await _reviewDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Review entity)
        {
            await _reviewDal.DeleteAsync(entity);
        }

        public async Task<Review> TGetByIdAsync(int id)
        {
            return await _reviewDal.GetByIdAsync(id);
        }

        public async Task<List<Review>> TGetListAllAsync()
        {
            return await _reviewDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Review entity)
        {
            await _reviewDal.UpdateAsync(entity);
        }
    }
}

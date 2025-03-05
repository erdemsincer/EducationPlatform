using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class TestimonialManager : ITestimonaiService
    {
        private readonly ITestimonialDal _testimonialDal;

        public TestimonialManager(ITestimonialDal testimonialDal)
        {
            _testimonialDal = testimonialDal;
        }

        public async Task TAddAsync(Testimonial entity)
        {
            await _testimonialDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Testimonial entity)
        {
            await _testimonialDal.DeleteAsync(entity);
        }

        public async Task<Testimonial> TGetByIdAsync(int id)
        {
            return await _testimonialDal.GetByIdAsync(id);
        }

        public async Task<List<Testimonial>> TGetListAllAsync()
        {
            return await _testimonialDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Testimonial entity)
        {
            await _testimonialDal.UpdateAsync(entity);
        }
    }
}

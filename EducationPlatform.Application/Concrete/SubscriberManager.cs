using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class SubscriberManager : ISubscriberService
    {
        private readonly ISubscriberDal _subscriberDal;

        public SubscriberManager(ISubscriberDal subscriberDal)
        {
            _subscriberDal = subscriberDal;
        }

        public async Task TAddAsync(Subscriber entity)
        {
            await _subscriberDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Subscriber entity)
        {
            await _subscriberDal.DeleteAsync(entity);
        }

        public async Task<Subscriber> TGetByIdAsync(int id)
        {
            return await _subscriberDal.GetByIdAsync(id);
        }

        public async Task<List<Subscriber>> TGetListAllAsync()
        {
            return await _subscriberDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Subscriber entity)
        {
            await _subscriberDal.UpdateAsync(entity);
        }
    }
}

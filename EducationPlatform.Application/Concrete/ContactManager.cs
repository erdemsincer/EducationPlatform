using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class ContactManager : IContactService
    {
        private readonly IContactDal _contactDal;

        public ContactManager(IContactDal contactDal)
        {
            _contactDal = contactDal;
        }

        public async Task TAddAsync(Contact entity)
        {
            await _contactDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Contact entity)
        {
            await _contactDal.DeleteAsync(entity);
        }

        public async Task<Contact> TGetByIdAsync(int id)
        {
            return await _contactDal.GetByIdAsync(id);
        }

        public async Task<List<Contact>> TGetListAllAsync()
        {
            return await _contactDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Contact entity)
        {
            await _contactDal.UpdateAsync(entity);
        }
    }
}

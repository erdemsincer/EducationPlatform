using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public async Task TAddAsync(Message entity)
        {
            await _messageDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Message entity)
        {
            await _messageDal.DeleteAsync(entity);
        }

        public async Task<Message> TGetByIdAsync(int id)
        {
            return await _messageDal.GetByIdAsync(id);
        }

        public async Task<List<Message>> TGetListAllAsync()
        {
            return await _messageDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Message entity)
        {
            await _messageDal.UpdateAsync(entity);
        }
    }
}

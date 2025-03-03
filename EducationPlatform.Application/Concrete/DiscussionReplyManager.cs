using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class DiscussionReplyManager : IDiscussionReplyService
    {
        private readonly IDiscussionReplyDal _discussionReplyDal;

        public DiscussionReplyManager(IDiscussionReplyDal discussionReplyDal)
        {
            _discussionReplyDal = discussionReplyDal;
        }

        public async Task TAddAsync(DiscussionReply entity)
        {
            await _discussionReplyDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(DiscussionReply entity)
        {
            await _discussionReplyDal.DeleteAsync(entity);
        }

        public async Task<DiscussionReply> TGetByIdAsync(int id)
        {
            return await _discussionReplyDal.GetByIdAsync(id);
        }

        public async Task<List<DiscussionReply>> TGetListAllAsync()
        {
            return await _discussionReplyDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(DiscussionReply entity)
        {
            await _discussionReplyDal.UpdateAsync(entity);
        }
        public async Task<List<DiscussionReply>> GetRepliesWithUserAsync(int discussionId)
        {
            return await _discussionReplyDal.GetRepliesWithUserAsync(discussionId);
        }

    }
}

using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class DiscussionManager : IDiscussionService
    {
        private readonly IDiscussionDal _discussionDal;

        public DiscussionManager(IDiscussionDal discussionDal)
        {
            _discussionDal = discussionDal;
        }

        public async Task TAddAsync(Discussion entity)
        {
            await _discussionDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Discussion entity)
        {
            await _discussionDal.DeleteAsync(entity);
        }

        public async Task<Discussion> TGetByIdAsync(int id)
        {
            return await _discussionDal.GetByIdAsync(id);
        }

        public async Task<List<Discussion>> TGetListAllAsync()
        {
            return await _discussionDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Discussion entity)
        {
            await _discussionDal.UpdateAsync(entity);
        }
        public async Task<List<Discussion>> GetDiscussionsWithUserAsync()
        {
            return await _discussionDal.GetDiscussionsWithUserAsync();
        }
        public async Task<List<Discussion>> GetDiscussionsByUserIdAsync(int userId)
        {
            return await _discussionDal.GetDiscussionsByUserIdAsync(userId);
        }
        public async Task<List<Discussion>> GetLastDiscussionsAsync(int count)
        {
            return await _discussionDal.GetLastDiscussionsAsync(count);
        }
        public async Task<List<Discussion>> GetDiscussionsWithUserAndReplyCountAsync()
        {
            return await _discussionDal.GetDiscussionsWithUserAndReplyCountAsync();
        }
        public async Task<Discussion> GetDiscussionWithRepliesByIdAsync(int id)
        {
            return await _discussionDal.GetDiscussionWithRepliesByIdAsync(id);
        }




    }
}

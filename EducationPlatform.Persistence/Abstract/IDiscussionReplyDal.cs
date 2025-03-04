using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IDiscussionReplyDal:IGenericDal<DiscussionReply>
    {
        Task<List<DiscussionReply>> GetRepliesWithUserAsync(int discussionId);
    

    }
}

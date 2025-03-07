using EducationPlatform.Domain.Entities;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IDiscussionDal : IGenericDal<Discussion>
    {
        Task<List<Discussion>> GetDiscussionsWithUserAsync();
        Task<List<Discussion>> GetDiscussionsByUserIdAsync(int userId);
        Task<List<Discussion>> GetLastDiscussionsAsync(int count);
        Task<List<Discussion>> GetDiscussionsWithUserAndReplyCountAsync();
        Task<Discussion> GetDiscussionWithRepliesByIdAsync(int id);





    }
}

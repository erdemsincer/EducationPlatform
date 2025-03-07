using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IDiscussionService:IGenericService<Discussion>
    {
        Task<List<Discussion>> GetDiscussionsWithUserAsync();
        Task<List<Discussion>> GetDiscussionsByUserIdAsync(int userId);
        Task<List<Discussion>> GetLastDiscussionsAsync(int count);
        Task<List<Discussion>> GetDiscussionsWithUserAndReplyCountAsync();
        Task<Discussion> GetDiscussionWithRepliesByIdAsync(int id);




    }
}

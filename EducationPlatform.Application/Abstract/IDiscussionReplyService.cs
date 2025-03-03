using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IDiscussionReplyService : IGenericService<DiscussionReply>
    {
        Task<List<DiscussionReply>> GetRepliesWithUserAsync(int discussionId);
    }
}

using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface ICommentService : IGenericService<Comment>
    {
        Task<List<Comment>> GetByResourceIdAsync(int resourceId);
        Task<List<Comment>> GetCommentsByUserIdAsync(int userId);

    }
}

using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface ICommentDal : IGenericDal<Comment>
    {
        Task<List<Comment>> GetByResourceIdAsync(int resourceId);
        Task<List<Comment>> GetCommentsByUserIdAsync(int userId);

    }
}

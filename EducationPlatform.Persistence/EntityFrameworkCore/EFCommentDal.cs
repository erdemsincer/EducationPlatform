using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public EFCommentDal(ApplicationDbContext context) : base(context)
        {
        }
    }
}

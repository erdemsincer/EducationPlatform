using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using EducationPlatform.Persistence.Repositories;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFCommentDal : GenericRepository<Comment>, ICommentDal
    {
        private readonly ApplicationDbContext _context;

        public EFCommentDal(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<Comment> GetByResourceId(int resourceId)
        {
            return _context.Comments.Where(c => c.ResourceId == resourceId).ToList();
        }
    }
}

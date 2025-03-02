using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public async Task<List<Comment>> GetByResourceIdAsync(int resourceId)
        {
            return await _commentDal.GetByResourceIdAsync(resourceId);
        }

        public async Task TAddAsync(Comment entity)
        {
            await _commentDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Comment entity)
        {
            await _commentDal.DeleteAsync(entity);
        }

        public async Task<Comment> TGetByIdAsync(int id)
        {
            return await _commentDal.GetByIdAsync(id);
        }

        public async Task<List<Comment>> TGetListAllAsync()
        {
            return await _commentDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Comment entity)
        {
            await _commentDal.UpdateAsync(entity);
        }
        public async Task<List<Comment>> GetCommentsByUserIdAsync(int userId)
        {
            return await _commentDal.GetCommentsByUserIdAsync(userId);
        }

    }
}

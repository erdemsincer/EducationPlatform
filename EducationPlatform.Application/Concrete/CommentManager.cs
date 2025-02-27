using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

namespace EducationPlatform.Application.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public List<Comment> GetByResourceId(int resourceId)
        {
            return _commentDal.GetByResourceId(resourceId);
        }

        public void TAdd(Comment entity)
        {
            _commentDal.Add(entity);
        }

        public void TDelete(Comment entity)
        {
            _commentDal.Delete(entity);
        }

        public Comment TGetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> TGetListAll()
        {
            return _commentDal.GetListAll();
        }

        public void TUpdate(Comment entity)
        {
            _commentDal.Update(entity);
        }
    }
}

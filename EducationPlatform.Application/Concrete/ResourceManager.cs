using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

namespace EducationPlatform.Application.Concrete
{
    public class ResourceManager : IResourceService
    {
        private readonly IResourceDal _resourceDal;

        public ResourceManager(IResourceDal resourceDal)
        {
            _resourceDal = resourceDal;
        }

        public List<Resource> GetByCategoryId(int categoryId)
        {
           return _resourceDal.GetByCategoryId(categoryId);
        }

        public void TAdd(Resource entity)
        {
            _resourceDal.Add(entity);
        }

        public void TDelete(Resource entity)
        {
            _resourceDal.Delete(entity);
        }

        public Resource TGetById(int id)
        {
            return _resourceDal.GetById(id);
        }

        public List<Resource> TGetListAll()
        {
            return _resourceDal.GetListAll();
        }

        public void TUpdate(Resource entity)
        {
            _resourceDal.Update(entity);
        }
    }
}

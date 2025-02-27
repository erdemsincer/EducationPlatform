using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

namespace EducationPlatform.Application.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public void TAdd(User entity)
        {
           _userDal.Add(entity);
        }

        public void TDelete(User entity)
        {
            _userDal.Delete(entity);
        }

        public User TGetById(int id)
        {
            return _userDal.GetById(id);
        }

        public List<User> TGetListAll()
        {
            return _userDal.GetListAll();
        }

        public void TUpdate(User entity)
        {
            _userDal.Update(entity);
        }
    }
}

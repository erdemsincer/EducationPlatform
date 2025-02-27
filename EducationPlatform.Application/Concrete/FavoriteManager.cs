using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

namespace EducationPlatform.Application.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private readonly IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }

        public void TAdd(Favorite entity)
        {
            _favoriteDal.Add(entity);
        }

        public void TDelete(Favorite entity)
        {
            _favoriteDal.Delete(entity);
        }

        public Favorite TGetById(int id)
        {
            return _favoriteDal.GetById(id);
        }

        public List<Favorite> TGetListAll()
        {
            return _favoriteDal.GetListAll();
        }

        public void TUpdate(Favorite entity)
        {
            _favoriteDal.Update(entity);
        }
    }
}

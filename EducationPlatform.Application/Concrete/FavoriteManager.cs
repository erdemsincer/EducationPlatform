using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class FavoriteManager : IFavoriteService
    {
        private readonly IFavoriteDal _favoriteDal;

        public FavoriteManager(IFavoriteDal favoriteDal)
        {
            _favoriteDal = favoriteDal;
        }

        public async Task<List<Favorite>> GetByUserIdAsync(int userId)
        {
            return await _favoriteDal.GetByUserIdAsync(userId);
        }

        public async Task TAddAsync(Favorite entity)
        {
            await _favoriteDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(Favorite entity)
        {
            await _favoriteDal.DeleteAsync(entity);
        }

        public async Task<Favorite> TGetByIdAsync(int id)
        {
            return await _favoriteDal.GetByIdAsync(id);
        }

        public async Task<List<Favorite>> TGetListAllAsync()
        {
            return await _favoriteDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(Favorite entity)
        {
            await _favoriteDal.UpdateAsync(entity);
        }
        // FavoriteManager.cs
        public async Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId)
        {
            return await _favoriteDal.GetFavoritesByUserIdAsync(userId);
        }

    }
}

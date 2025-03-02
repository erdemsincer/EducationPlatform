using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IFavoriteDal : IGenericDal<Favorite>
    {
        Task<List<Favorite>> GetByUserIdAsync(int userId);
        Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId);
    }
}

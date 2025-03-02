using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IFavoriteService : IGenericService<Favorite>
    {
        Task<List<Favorite>> GetByUserIdAsync(int userId);
        // IFavoriteService.cs
        Task<List<Favorite>> GetFavoritesByUserIdAsync(int userId);

    }
}

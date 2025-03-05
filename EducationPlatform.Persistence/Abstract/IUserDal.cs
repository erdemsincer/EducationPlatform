using EducationPlatform.Domain.Entities;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface IUserDal : IGenericDal<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserWithRolesByEmailAsync(string email);
    }
}

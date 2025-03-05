using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task TAddAsync(User entity)
        {
            await _userDal.AddAsync(entity);
        }

        public async Task TDeleteAsync(User entity)
        {
            await _userDal.DeleteAsync(entity);
        }

        public async Task<User> TGetByIdAsync(int id)
        {
            return await _userDal.GetByIdAsync(id);
        }

        public async Task<List<User>> TGetListAllAsync()
        {
            return await _userDal.GetListAllAsync();
        }

        public async Task TUpdateAsync(User entity)
        {
            await _userDal.UpdateAsync(entity);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userDal.GetByEmailAsync(email);
        }
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userDal.GetUserByIdAsync(id);
        }

        public async  Task<User> GetUserWithRolesByEmailAsync(string email)
        {
            return await _userDal.GetUserWithRolesByEmailAsync(email);
        }
    }
}

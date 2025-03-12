using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Concrete
{
    public class SkillManager : ISkillService
    {
        private readonly ISkillDal _skillDal;

        public SkillManager(ISkillDal skillDal)
        {
            _skillDal = skillDal;
        }

        // Kullanıcının becerilerini almak
        public async Task<List<Skill>> GetSkillsByUserAsync(int userId)
        {
            return await _skillDal.GetByUserIdAsync(userId);
        }

        // Beceriyi eklemek
        public async Task TAddAsync(Skill entity)
        {
            await _skillDal.AddAsync(entity);
        }

        // Beceriyi silmek
        public async Task TDeleteAsync(Skill entity)
        {
            await _skillDal.DeleteAsync(entity);
        }

        // Beceriyi ID ile almak
        public async Task<Skill> TGetByIdAsync(int id)
        {
            return await _skillDal.GetByIdAsync(id);
        }

        // Bütün becerileri listelemek
        public async Task<List<Skill>> TGetListAllAsync()
        {
            return await _skillDal.GetListAllAsync();
        }

        // Beceriyi güncellemek
        public async Task TUpdateAsync(Skill entity)
        {
            await _skillDal.UpdateAsync(entity);
            
        }
    }
}

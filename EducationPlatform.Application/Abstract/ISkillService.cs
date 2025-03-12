using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface ISkillService:IGenericService<Skill>
    {
        Task<List<Skill>> GetSkillsByUserAsync(int userId);

    }
}

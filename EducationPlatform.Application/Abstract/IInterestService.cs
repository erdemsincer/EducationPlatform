using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IInterestService:IGenericService<Interest>
    {
        Task<List<Interest>> GetInterestsByUserAsync(int userId);


    }
}

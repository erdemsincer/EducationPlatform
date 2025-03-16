using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface ICareerTestAnswerDal
    {
        Task SaveUserAnswersAsync(int userId, List<CareerTestAnswer> answers);
        Task<List<CareerTestAnswer>> GetUserAnswersAsync(int userId);
    }
}

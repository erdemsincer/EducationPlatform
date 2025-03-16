using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface ICareerTestQuestionService
    {
        Task<List<CareerTestQuestion>> GetAllQuestionsAsync(); // Tüm test sorularını getir
        Task<CareerTestQuestion> GetQuestionByIdAsync(int questionId); // ID'ye göre test sorusu getir
    }
}

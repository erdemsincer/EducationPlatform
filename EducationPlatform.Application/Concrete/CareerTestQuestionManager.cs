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
    public class CareerTestQuestionManager : ICareerTestQuestionService
    {
        private readonly ICareerTestQuestionDal _careerTestQuestionDal;

        public CareerTestQuestionManager(ICareerTestQuestionDal careerTestQuestionDal)
        {
            _careerTestQuestionDal = careerTestQuestionDal;
        }

        public async Task<List<CareerTestQuestion>> GetAllQuestionsAsync()
        {
            return await _careerTestQuestionDal.GetAllQuestionsAsync();
        }

        public async Task<CareerTestQuestion> GetQuestionByIdAsync(int questionId)
        {
            return await _careerTestQuestionDal.GetQuestionByIdAsync(questionId);
        }
    }
}

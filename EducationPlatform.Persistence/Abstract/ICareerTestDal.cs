using EducationPlatform.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.Abstract
{
    public interface ICareerTestDal
    {
        Task<List<CareerTest>> GetAllQuestionsAsync(); // Soruları getir
        Task SaveUserAnswersAsync(List<CareerTest> careerTests); // Kullanıcı cevaplarını kaydet
        Task<List<CareerTest>> GetUserAnswers(int userId); // Kullanıcının verdiği yanıtları getir
    }
}

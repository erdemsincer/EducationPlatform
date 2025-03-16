using EducationPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface ICareerTestAnswerService
    {
        Task SaveUserAnswersAsync(int userId, Dictionary<int, string> answers); // Kullanıcı cevaplarını kaydet
        Task<List<CareerTestAnswer>> GetUserAnswersAsync(int userId); // Kullanıcının verdiği cevapları getir
        Task<string> GetCareerAdviceAsync(int userId); // Kullanıcının cevaplarına göre öneri al
    }
}

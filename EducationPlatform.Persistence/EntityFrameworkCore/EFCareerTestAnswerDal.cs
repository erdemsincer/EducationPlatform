using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFCareerTestAnswerDal : ICareerTestAnswerDal
    {
        private readonly ApplicationDbContext _context;

        public EFCareerTestAnswerDal(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kullanıcı cevaplarını veritabanına kaydetme
        public async Task SaveUserAnswersAsync(int userId, List<CareerTestAnswer> answers)
        {
            if (answers == null || !answers.Any())
            {
                throw new ArgumentException("Cevaplar geçersiz!");
            }

            foreach (var answer in answers)
            {
                answer.UserId = userId;

                // **Önce aynı kullanıcı ve aynı soru için daha önce kayıt olup olmadığını kontrol et**
                var existingAnswer = await _context.CareerTestAnswers
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.QuestionId == answer.QuestionId);

                if (existingAnswer != null)
                {
                    // **Eğer aynı kullanıcı daha önce bu soruya cevap vermişse, cevabı güncelle**
                    existingAnswer.SelectedAnswer = answer.SelectedAnswer;
                    _context.CareerTestAnswers.Update(existingAnswer);
                }
                else
                {
                    // **Eğer kayıt yoksa, yeni bir cevap ekle**
                    await _context.CareerTestAnswers.AddAsync(answer);
                }
            }

            // **Değişiklikleri kaydet**
            await _context.SaveChangesAsync();
        }

        // Kullanıcının tüm cevaplarını alma
        public async Task<List<CareerTestAnswer>> GetUserAnswersAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Geçersiz kullanıcı ID");
            }

            return await _context.CareerTestAnswers
                .Where(a => a.UserId == userId)  // Belirtilen kullanıcıya ait cevapları getir
                .ToListAsync();
        }
    }
}

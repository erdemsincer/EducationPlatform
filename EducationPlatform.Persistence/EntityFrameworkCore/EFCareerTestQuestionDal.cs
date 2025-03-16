using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Persistence.EntityFrameworkCore
{
    public class EFCareerTestQuestionDal : ICareerTestQuestionDal
    {
        private readonly ApplicationDbContext _context;

        public EFCareerTestQuestionDal(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CareerTestQuestion>> GetAllQuestionsAsync()
        {
            return await _context.CareerTestQuestions.ToListAsync();
        }

        public async Task<CareerTestQuestion> GetQuestionByIdAsync(int questionId)
        {
            return await _context.CareerTestQuestions.FindAsync(questionId);
        }
    }
}

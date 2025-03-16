using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;
using EducationPlatform.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class EFCareerTestDal : ICareerTestDal
{
    private readonly ApplicationDbContext _context;

    public EFCareerTestDal(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<CareerTest>> GetAllQuestionsAsync()
    {
        return await _context.CareerTests
            .Where(q => q.SelectedAnswer == null) // Sadece soruları getir
            .ToListAsync();
    }

    public async Task SaveUserAnswersAsync(List<CareerTest> careerTests)
    {
        await _context.CareerTests.AddRangeAsync(careerTests);
        await _context.SaveChangesAsync();
    }

    public async Task<List<CareerTest>> GetUserAnswers(int userId)
    {
        return await _context.CareerTests
            .Where(q => q.UserId == userId && q.SelectedAnswer != null)
            .ToListAsync();
    }
}

using EducationPlatform.Application.Abstract;
using EducationPlatform.Domain.Entities;
using EducationPlatform.Persistence.Abstract;

public class CareerTestAnswerManager : ICareerTestAnswerService
{
    private readonly ICareerTestAnswerDal _careerTestAnswerDal;
    private readonly OpenAiService _openAiService;

    public CareerTestAnswerManager(ICareerTestAnswerDal careerTestAnswerDal, OpenAiService openAiService)
    {
        _careerTestAnswerDal = careerTestAnswerDal;
        _openAiService = openAiService;
    }

    // Save user answers correctly
    public async Task SaveUserAnswersAsync(int userId, Dictionary<int, string> answers)
    {
        if (answers == null || answers.Count == 0)
        {
            throw new ArgumentException("Cevaplar geçersiz!");
        }

        // Convert Dictionary to List<CareerTestAnswer>
        var careerTestAnswers = answers.Select(a => new CareerTestAnswer
        {
            UserId = userId,
            QuestionId = a.Key,
            SelectedAnswer = a.Value
        }).ToList();

        // Save to database
        await _careerTestAnswerDal.SaveUserAnswersAsync(userId, careerTestAnswers);
    }

    // Get user answers
    public async Task<List<CareerTestAnswer>> GetUserAnswersAsync(int userId)
    {
        return await _careerTestAnswerDal.GetUserAnswersAsync(userId);
    }

    // Get career advice based on user's answers
    public async Task<string> GetCareerAdviceAsync(int userId)
    {
        var userAnswers = await _careerTestAnswerDal.GetUserAnswersAsync(userId);
        if (userAnswers == null || !userAnswers.Any())
            return "Test cevapları eksik!";

        string formattedAnswers = string.Join(", ", userAnswers.Select(a => a.SelectedAnswer));
        return await _openAiService.GetCareerAdvice(formattedAnswers, "", "");
    }
}

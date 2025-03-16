
using EducationPlatform.Persistence.Abstract;

public class ChatbotManager : IChatbotService
{
    private readonly IChatbotDal _chatbotDal;
    private readonly OpenAiService _openAiService;
    private readonly ICareerTestAnswerDal _careerTestAnswerDal;

    public ChatbotManager(IChatbotDal chatbotDal, OpenAiService openAiService)
    {
        _chatbotDal = chatbotDal;
        _openAiService = openAiService;
    }

    public async Task<string> GetCareerAdviceAsync(int userId)
    {
        var user = await _chatbotDal.GetUserWithDetailsAsync(userId);

        if (user == null)
            return "Kullanıcı bulunamadı.";

        // **Beceriler, ilgi alanları ve kariyer hedeflerini düzgün çekelim**
        string skills = user.Skills != null && user.Skills.Any()
            ? string.Join(", ", user.Skills.Select(s => $"{s.SkillName} ({s.ProficiencyLevel})"))
            : "Beceri bilgisi bulunamadı.";

        string interests = user.Interests != null && user.Interests.Any()
            ? string.Join(", ", user.Interests.Select(i => i.InterestName))
            : "İlgi alanı bilgisi bulunamadı.";

        string careerGoals = user.CareerGoals != null && user.CareerGoals.Any()
            ? string.Join(", ", user.CareerGoals.Select(c => c.GoalName))
            : "Kariyer hedefi bilgisi bulunamadı.";

        // **Boş veri gönderilmediğinden emin ol**
        return await _openAiService.GetCareerAdvice(skills, interests, careerGoals);
    }
    public async Task<string> GetCareerAdviceFromTestAsync(int userId, string formattedAnswers)
    {
        // **Test cevapları string olarak alındı, doğrudan OpenAiService'e gönderilebilir**
        if (string.IsNullOrEmpty(formattedAnswers))
            return "Test cevapları eksik!";

        // **AI'ye gönderilecek prompt**
        return await _openAiService.GetCareerAdviceFromTest(formattedAnswers);
    }


}

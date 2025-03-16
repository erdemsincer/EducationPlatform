using EducationPlatform.Dto.Chat;
using EducationPlatform.Dto.CareerTest;
using Microsoft.AspNetCore.Mvc;
using EducationPlatform.Application.Abstract;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/chatbot")]
public class ChatBotController : ControllerBase
{
    private readonly IChatbotService _chatbotService;
    private readonly ICareerTestAnswerService _careerTestAnswerService;

    public ChatBotController(IChatbotService chatbotService, ICareerTestAnswerService careerTestAnswerService)
    {
        _chatbotService = chatbotService;
        _careerTestAnswerService = careerTestAnswerService;
    }

    // 📌 **Kariyer önerisini almak için mevcut kullanıcı ID'si üzerinden**
    [HttpPost("career-advice")]
    public async Task<IActionResult> GetCareerAdvice([FromBody] CareerRequest request)
    {
        if (request == null || request.UserId <= 0)
        {
            return BadRequest(new { message = "Geçersiz kullanıcı ID." });
        }

        // **Kariyer önerisi alma işlemi**
        var result = await _chatbotService.GetCareerAdviceAsync(request.UserId);

        if (string.IsNullOrEmpty(result))
        {
            return BadRequest(new { message = "Kariyer önerisi alınamadı." });
        }

        return Ok(new EducationPlatform.Dto.Chat.CareerAdviceResponseDto { CareerAdvice = result });
    }

    // 📌 **Kariyer testi cevapları üzerinden kariyer önerisi almak için yeni bir endpoint**
    [HttpPost("career-advice-from-test")]
    public async Task<IActionResult> GetCareerAdviceFromTest([FromBody] CareerRequest request)
    {
        if (request == null || request.UserId <= 0)
        {
            return BadRequest(new { message = "Geçersiz kullanıcı ID." });
        }

        // **Kariyer testi cevaplarını alıyoruz**
        var userAnswers = await _careerTestAnswerService.GetUserAnswersAsync(request.UserId);

        if (userAnswers == null || !userAnswers.Any())
        {
            return BadRequest(new { message = "Kariyer testi cevapları alınamadı." });
        }

        // **Kariyer testi cevaplarını formatlayalım**
        string formattedAnswers = string.Join(", ", userAnswers.Select(a => $"Soru {a.QuestionId}: {a.SelectedAnswer}"));

        // **AI'ya gönderilecek olan prompt'u oluşturuyoruz**
        var result = await _chatbotService.GetCareerAdviceFromTestAsync(request.UserId, formattedAnswers);

        if (string.IsNullOrEmpty(result))
        {
            return BadRequest(new { message = "AI'dan yanıt alınamadı." });
        }

        return Ok(new EducationPlatform.Dto.CareerTest.CareerAdviceResponseDto { CareerAdvice = result });
    }

}

public class CareerRequest
{
    public int UserId { get; set; }
}

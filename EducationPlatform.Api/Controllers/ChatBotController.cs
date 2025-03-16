using EducationPlatform.Dto.Chat;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chatbot")]
public class ChatBotController : ControllerBase
{
    private readonly IChatbotService _chatbotService;

    public ChatBotController(IChatbotService chatbotService)
    {
        _chatbotService = chatbotService;
    }

    [HttpPost("career-advice")]
    public async Task<IActionResult> GetCareerAdvice([FromBody] CareerRequest request)
    {
        if (request == null || request.UserId <= 0)
        {
            Console.WriteLine("🔴 HATA: Geçersiz kullanıcı ID.");
            return BadRequest(new { message = "Geçersiz kullanıcı ID." });
        }

        Console.WriteLine($"✅ API Çağrıldı: /api/chatbot/career-advice UserId={request.UserId}");

        var result = await _chatbotService.GetCareerAdviceAsync(request.UserId);

        if (string.IsNullOrEmpty(result))
        {
            Console.WriteLine("🔴 HATA: API'den boş yanıt alındı!");
            return BadRequest(new { message = "Kariyer önerisi alınamadı." });
        }

        Console.WriteLine($"✅ API Yanıtı: {result}");

        return Ok(new CareerAdviceResponseDto { CareerAdvice = result });

    }
}

public class CareerRequest
{
    public int UserId { get; set; }
}

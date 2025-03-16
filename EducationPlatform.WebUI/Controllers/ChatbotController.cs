using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EducationPlatform.Dto.Chat;

public class ChatbotController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ChatbotController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IActionResult GetCareerAdvice()
    {
        Console.WriteLine("✅ GetCareerAdvice Sayfası Yükleniyor...");
        return View();  // Sayfayı render et
    }

    [HttpPost]
    public async Task<IActionResult> GetCareerAdvicePost()
    {
        try
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Öneri alabilmek için giriş yapmalısınız!";
                return RedirectToAction("Auth", "Login");
            }

            Console.WriteLine($"✅ Kullanıcı ID: {userId}");

            var client = _httpClientFactory.CreateClient();
            var requestBody = new StringContent(JsonConvert.SerializeObject(new { UserId = int.Parse(userId) }), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:7028/api/chatbot/career-advice", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"🔴 API BAŞARISIZ! StatusCode: {response.StatusCode}");
                TempData["ErrorMessage"] = "Kariyer önerisi alınırken hata oluştu.";
                return RedirectToAction("GetCareerAdvice");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"✅ API Yanıtı (Ham Veri): {jsonData}");

            // JSON verisini deserialize et
            var careerAdviceResponse = JsonConvert.DeserializeObject<CareerAdviceResponseDto>(jsonData);

            if (careerAdviceResponse == null || string.IsNullOrEmpty(careerAdviceResponse.CareerAdvice))
            {
                Console.WriteLine("🔴 API'den Boş Yanıt Alındı!");
                TempData["ErrorMessage"] = "Kariyer önerisi alınamadı.";
                return RedirectToAction("GetCareerAdvice");
            }

            Console.WriteLine($"✅ API Yanıtı (Parsed): {careerAdviceResponse.CareerAdvice}");

            // ViewBag yerine TempData kullan!
            TempData["CareerAdvice"] = careerAdviceResponse.CareerAdvice;
            return RedirectToAction("GetCareerAdvice");  // Sayfayı tekrar yükle
        }
        catch (Exception ex)
        {
            Console.WriteLine($"🔴 BEKLENMEYEN HATA! {ex.Message}");
            TempData["ErrorMessage"] = $"Beklenmeyen bir hata oluştu! {ex.Message}";
            return RedirectToAction("GetCareerAdvice");
        }
    }
}

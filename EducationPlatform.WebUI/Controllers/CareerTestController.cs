using EducationPlatform.Dto.Chat;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using EducationPlatform.Dto.CareerTest;
using CareerAdviceResponseDto = EducationPlatform.Dto.CareerTest.CareerAdviceResponseDto;

namespace EducationPlatform.WebUI.Controllers
{
    public class CareerTestController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CareerTestController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 📌 **Kariyer Testi Soruları**
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:7028/api/career-test/questions");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Kariyer testini alırken hata oluştu!";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                TempData["ErrorMessage"] = "Kariyer testi soruları alınamadı.";
                return RedirectToAction("Index");
            }

            try
            {
                // Soruları deserialize et
                var questions = JsonConvert.DeserializeObject<List<CareerTestQuestionDto>>(jsonData);
                return View(questions);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"JSON ayrıştırma hatası: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        // 📌 **Kullanıcının verdiği cevapları kaydetme**
        [HttpPost]
        public async Task<IActionResult> SubmitAnswers(CareerTestAnswersDto answersDto)
        {
            if (answersDto == null || answersDto.Answers == null || answersDto.Answers.Count == 0)
            {
                TempData["ErrorMessage"] = "Cevaplar eksik!";
                return RedirectToAction("Index");
            }

            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "Kullanıcı girişi yapılmamış!";
                return RedirectToAction("Index");
            }

            answersDto.UserId = int.Parse(userId);  // Kullanıcı ID'sini ekliyoruz

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://localhost:7028/api/career-test/submit-answers", answersDto);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Cevaplar başarıyla kaydedildi.";
                return RedirectToAction("GetCareerAdviceFromTest", new { userId = userId });
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = $"Cevaplar kaydedilirken hata oluştu! {errorMessage}";
                return RedirectToAction("Index");
            }
        }

        // 📌 **Kariyer önerisini almak** (GET request for userId)
        [HttpGet]
        public async Task<IActionResult> GetCareerAdviceFromTest(int userId)
        {
            if (string.IsNullOrEmpty(userId.ToString()))
            {
                TempData["ErrorMessage"] = "Kullanıcı girişi yapılmamış!";
                return RedirectToAction("Index");
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://localhost:7028/api/chatbot/career-advice-from-test", new { UserId = userId });

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Kariyer önerisi alınırken hata oluştu!";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(jsonData))
            {
                TempData["ErrorMessage"] = "Kariyer önerisi alınamadı.";
                return RedirectToAction("Index");
            }

            // Deserialize the response into a CareerAdviceResponseDto object
            var careerAdviceResponse = JsonConvert.DeserializeObject<CareerAdviceResponseDto>(jsonData);
            TempData["SuccessMessage"] = "Kariyer önerisi başarıyla alındı.";

            // Pass the career advice to the view
            return RedirectToAction("DisplayCareerAdvice", new { advice = careerAdviceResponse.CareerAdvice });
        }

        // 📌 **Kariyer önerisini göster**
        public IActionResult DisplayCareerAdvice(string advice)
        {
            if (string.IsNullOrEmpty(advice))
            {
                TempData["ErrorMessage"] = "Kariyer önerisi alınamadı.";
                return RedirectToAction("Index");
            }

            // Return the advice in the view
            return View("DisplayCareerAdvice", new CareerAdviceResponseDto { CareerAdvice = advice });
        }
    }
}

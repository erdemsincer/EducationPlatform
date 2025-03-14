using EducationPlatform.Dto.InstructorDto;
using EducationPlatform.Dto.ReviewDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace EducationPlatform.WebUI.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public InstructorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // 🔹 API üzerinden TÜM Eğitmenleri Listele
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://localhost:7028/api/Instructor");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Eğitmenler yüklenirken hata oluştu!";
                return View(new List<ResultInstructorDto>()); // Boş liste döndür
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var instructors = JsonConvert.DeserializeObject<List<ResultInstructorDto>>(jsonData);

            return View(instructors);
        }

        // 🔹 API üzerinden Eğitmenin Detaylarını ve Yorumlarını Getir
        public async Task<IActionResult> InstructorDetails(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:7028/api/Instructor/details/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Eğitmen bilgileri yüklenirken hata oluştu!";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API'den Gelen JSON:");
            Console.WriteLine(jsonData); // JSON Verisini Konsola Yazdır

            var instructorDetail = JsonConvert.DeserializeObject<InstructorWithReviewsDto>(jsonData);

            // Model.Reviews Listesini Kontrol Et
            if (instructorDetail.Reviews == null)
            {
                Console.WriteLine("Reviews listesi NULL geldi!");
            }
            else
            {
                Console.WriteLine($"Reviews Count: {instructorDetail.Reviews.Count}");
            }

            return View(instructorDetail);
        }


        // 🔹 Eğitmene Yeni Yorum Ekle
        [HttpPost]
        public async Task<IActionResult> AddReview(CreateReviewDto dto)
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["ErrorMessage"] = "Yorum yapabilmek için giriş yapmalısınız!";
                    return RedirectToAction("InstructorDetails", new { id = dto.InstructorId });
                }

                dto.UserId = int.Parse(userId); // Kullanıcı ID ekleniyor

                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://localhost:7028/api/Review/", dto);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Değerlendirme başarıyla eklendi.";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Değerlendirme eklenirken hata oluştu! {errorMessage}";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Sunucu hatası oluştu!";
            }

            return RedirectToAction("InstructorDetails", new { id = dto.InstructorId });
        }
    }
}

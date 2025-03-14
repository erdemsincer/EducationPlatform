using EducationPlatform.Dto.DiscussionDto;
using EducationPlatform.Dto.DiscussionReplyDto;
using EducationPlatform.Dto.SubscriberDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace EducationPlatform.WebUI.Controllers
{
    public class DiscussionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscussionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(CreateSubscriberDto model)
        {
            var client = _httpClientFactory.CreateClient();
            await client.PostAsJsonAsync("http://localhost:7028/api/Subscriber", model);
            return NoContent();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"http://localhost:7028/api/Discussion/GetDiscussionDetailWithReplies/{id}");

            // API isteği başarısızsa hata mesajı döndür ve ana sayfaya yönlendir
            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Tartışma yüklenirken hata oluştu!";
                return RedirectToAction("Index"); // Kullanıcıyı tartışmalar sayfasına yönlendir
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            // API'den boş yanıt geldiyse hata mesajı ver ve ana sayfaya yönlendir
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                TempData["ErrorMessage"] = "Bu tartışma için veri bulunamadı.";
                return RedirectToAction("Index");
            }

            try
            {
                var discussionDetail = JsonConvert.DeserializeObject<DiscussionWithRepliesDto>(jsonData);
                return View(discussionDetail);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"JSON ayrıştırma hatası: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddReply(CreateDiscussionReplyDto dto)
        {
            try
            {
                var userId = HttpContext.Session.GetString("UserId");
                if (string.IsNullOrEmpty(userId))
                {
                    TempData["ErrorMessage"] = "Yorum yapabilmek için giriş yapmalısınız!";
                    return RedirectToAction("Detail", new { id = dto.DiscussionId });
                }

                dto.UserId = int.Parse(userId); // Kullanıcı ID ekleniyor

                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://localhost:7028/api/DiscussionReply", dto);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Yorum başarıyla eklendi.";
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Yorum eklenirken hata oluştu! {errorMessage}";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Sunucu hatası oluştu!";
            }

            return RedirectToAction("Detail", new { id = dto.DiscussionId });
        }




    }
}

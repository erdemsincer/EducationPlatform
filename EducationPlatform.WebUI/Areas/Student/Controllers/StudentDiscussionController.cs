using EducationPlatform.Dto.DiscussionDto;
using EducationPlatform.Dto.DiscussionReplyDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EducationPlatform.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("Student/StudentDiscussion")]
    public class StudentDiscussionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentDiscussionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("http://localhost:7028/api/Discussion/GetAll");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışmalar yüklenemedi.";
                return View(new List<ResultDiscussionDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var discussions = JsonConvert.DeserializeObject<List<ResultDiscussionDto>>(jsonData);

            return View(discussions);
        }

        [HttpGet("CreateDiscussion")]
        public IActionResult CreateDiscussion()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });

            return View();
        }

        [HttpPost("CreateDiscussion")]
        public async Task<IActionResult> CreateDiscussion(CreateDiscussionDto dto)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });

            dto.UserId = int.Parse(userId);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:7028/api/Discussion", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışma oluşturulamadı!";
                return View(dto);
            }

            TempData["Success"] = "Tartışma başarıyla oluşturuldu!";
            return RedirectToAction("MyDiscussions");
        }

        [Route("MyDiscussions")]
        public async Task<IActionResult> MyDiscussions()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync($"http://localhost:7028/api/Discussion/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışmalar yüklenemedi.";
                return View(new List<ResultDiscussionDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var discussions = JsonConvert.DeserializeObject<List<ResultDiscussionDto>>(jsonData);

            return View(discussions);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.DeleteAsync($"http://localhost:7028/api/Discussion/{id}");

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Tartışma başarıyla silindi." : "Tartışma silinirken hata oluştu.";

            return RedirectToAction("MyDiscussions");
        }

        [HttpGet("Update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync($"http://localhost:7028/api/Discussion/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışma bilgisi alınamadı.";
                return RedirectToAction("MyDiscussions");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var discussion = JsonConvert.DeserializeObject<UpdateDiscussionDto>(jsonData);

            return View(discussion);
        }

        [HttpPost("UpdateDiscussion")]
        public async Task<IActionResult> Update(UpdateDiscussionDto dto)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });

            dto.UserId = int.Parse(userId);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(dto);
            Console.WriteLine($"✅ API'ye Gönderilen Veri: {jsonData}");

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("http://localhost:7028/api/Discussion", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                TempData["Error"] = $"Tartışma güncellenemedi! Detay: {errorMessage}";
                return View(dto);
            }

            TempData["Success"] = "Tartışma başarıyla güncellendi!";
            return RedirectToAction("MyDiscussions");
        }
        [HttpGet("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            return RedirectToAction("Detail", "StudentDiscussionReply", new { area = "Student", id });
        }



    }
}

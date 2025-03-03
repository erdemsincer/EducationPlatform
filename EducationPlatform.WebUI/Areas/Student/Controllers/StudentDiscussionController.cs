using EducationPlatform.Dto.DiscussionDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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

            var response = await client.GetAsync("https://localhost:7028/api/Discussion/GetAll");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışmalar yüklenemedi.";
                return View(new List<ResultDiscussionDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var discussions = JsonConvert.DeserializeObject<List<ResultDiscussionDto>>(jsonData);

            return View(discussions);
        }
        [Route("MyDiscussions")]
        public async Task<IActionResult> MyDiscussions()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync($"https://localhost:7028/api/Discussion/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışmalar yüklenemedi.";
                return View(new List<ResultDiscussionDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var discussions = JsonConvert.DeserializeObject<List<ResultDiscussionDto>>(jsonData);

            return View(discussions);
        }
    }
}

using EducationPlatform.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EducationPlatform.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("Student/StudentProfile")]
    public class StudentProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync($"https://localhost:7028/api/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Profil bilgileri alınamadı.";
                return RedirectToAction("Index", "Dashboard");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<ResultUserDto>(jsonData);

            return View(user);
        }
    }
}

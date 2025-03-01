using EducationPlatform.Dto.CategoryDto;
using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EducationPlatform.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("Student/StudentResource")]
    public class StudentResourceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentResourceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            ViewBag.UserId = userId;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });

            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync($"https://localhost:7028/api/Resource/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Kaynaklar yüklenirken bir hata oluştu.";
                return View(new List<ResultResourceDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var resources = JsonConvert.DeserializeObject<List<ResultResourceDto>>(jsonData);

            return View(resources);
        }
    }
}

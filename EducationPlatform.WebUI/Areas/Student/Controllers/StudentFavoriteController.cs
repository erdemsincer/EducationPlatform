using EducationPlatform.Dto.FavoriteDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EducationPlatform.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("Student/StudentFavorite")]
    public class StudentFavoriteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentFavoriteController(IHttpClientFactory httpClientFactory)
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

            var response = await client.GetAsync($"http://localhost:7028/api/Favorite/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "❌ Favoriler yüklenirken bir hata oluştu.";
                return View(new List<ResultFavoriteDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var favorites = JsonConvert.DeserializeObject<List<ResultFavoriteDto>>(jsonData);

            return View(favorites);
        }

        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.DeleteAsync($"http://localhost:7028/api/Favorite/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "❌ Favori silinemedi!";
                return RedirectToAction("Index");
            }

            TempData["Success"] = "✅ Favori başarıyla silindi!";
            return RedirectToAction("Index");
        }
    }
}

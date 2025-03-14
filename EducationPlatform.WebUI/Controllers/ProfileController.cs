using EducationPlatform.Dto.DiscussionDto;
using EducationPlatform.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EducationPlatform.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Kullanıcı profil bilgilerini görüntüleme
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login" });
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync($"http://localhost:7028/api/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Profil bilgileri alınamadı.";
                return RedirectToAction("Index", "Home");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<ResultUserDto>(jsonData);

            return View(user);
        }

        // Kullanıcı profil bilgilerini güncelleme sayfası (GET)
        public async Task<IActionResult> Update()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login" });
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync($"http://localhost:7028/api/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Profil bilgileri alınamadı.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UpdateUserDto>(jsonData);

            return View(user);
        }

        // Kullanıcı profil bilgilerini güncelleme işlemi (POST)
        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUserDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("http://localhost:7028/api/User/UpdateUser", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Profil güncellenirken hata oluştu.";
                return View(updateUserDto);
            }

            TempData["Success"] = "Profil başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
        
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

            var response = await client.GetAsync($"http://localhost:7028/api/Discussion/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışmalarınız yüklenirken bir hata oluştu.";
                return RedirectToAction("Index");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var discussions = JsonConvert.DeserializeObject<List<ResultDiscussionDto>>(jsonData);

            return View(discussions);
        }

    }
}

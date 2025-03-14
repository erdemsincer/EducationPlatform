using EducationPlatform.Dto.CategoryDto;
using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EducationPlatform.WebUI.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    [Area("Student")]
    [Route("Student/StudentResource")]
    public class StudentResourceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentResourceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private async Task LoadCategoryDropdown()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var response = await client.GetAsync("http://localhost:7028/api/Category");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Categories = new List<SelectListItem>();
                return;
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var categoryList = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);

            List<SelectListItem> categories = categoryList.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.Categories = categories;
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

            var response = await client.GetAsync($"http://localhost:7028/api/Resource/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.ErrorMessage = "Kaynaklar yüklenirken bir hata oluştu.";
                return View(new List<ResultResourceDto>());
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var resources = JsonConvert.DeserializeObject<List<ResultResourceDto>>(jsonData);

            return View(resources);
        }

        [HttpGet]
        [Route("CreateResource")]
        public async Task<IActionResult> CreateResource()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
            }

            await LoadCategoryDropdown();
            return View();
        }

        [HttpPost]
        [Route("CreateResource")]
        public async Task<IActionResult> CreateResource(CreateResourceDto createResourceDto)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
            }

            createResourceDto.UserId = int.Parse(userId); // Kullanıcı ID ekleniyor

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(createResourceDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:7028/api/Resource", content);

            if (!response.IsSuccessStatusCode)
            {
                // **📌 API'den Gelen Hata Mesajını Oku**
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = $"API Hatası: {errorMessage}";

                await LoadCategoryDropdown();
                return View(createResourceDto);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("DeleteResource/{id}")]
        public async Task<IActionResult> DeleteResource(int id)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
            }

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            Console.WriteLine($"🔹 Silme isteği gönderiliyor: /api/Resource/{id}");

            var response = await client.DeleteAsync($"http://localhost:7028/api/Resource/{id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"❌ Silme Hatası: {errorMessage}");
                TempData["ErrorMessage"] = "❌ Kaynak silinirken bir hata oluştu!";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "✅ Kaynak başarıyla silindi.";
            return RedirectToAction("Index");
        }


    }
}

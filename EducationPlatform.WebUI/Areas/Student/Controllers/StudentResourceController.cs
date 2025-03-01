using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

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
    
    // Eğer kullanıcı giriş yapmamışsa, login sayfasına yönlendir
    if (string.IsNullOrEmpty(userId))
    {
        return RedirectToAction("Login", "Auth");
    }

    // **✅ API'den kullanıcının kaynaklarını çek**
    var client = _httpClientFactory.CreateClient();
    client.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

    var response = await client.GetAsync($"https://localhost:7028/api/Resource/User/{userId}");

    if (!response.IsSuccessStatusCode)
    {
        ViewBag.ErrorMessage = "Kaynaklar yüklenirken bir hata oluştu.";
        return View(new List<ResultResourceDto>()); // Boş liste döndür
    }

    var jsonData = await response.Content.ReadAsStringAsync();
    var resources = JsonConvert.DeserializeObject<List<ResultResourceDto>>(jsonData);

    return View(resources);
        }





        public IActionResult CreateResource()
        {
            return View();
        }
    }
}

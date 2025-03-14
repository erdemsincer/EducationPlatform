using EducationPlatform.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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

            var response = await client.GetAsync($"http://localhost:7028/api/User/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Profil bilgileri alınamadı.";
                return RedirectToAction("Index", "Dashboard");
            }

            var jsonData = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<ResultUserDto>(jsonData);

            return View(user);
        }

        [Route("Update")]
        [HttpGet]
        public async Task<IActionResult> Update()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
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

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateUserDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("http://localhost:7028/api/User/UpdateUser", content);

           

           
            return RedirectToAction("Index");
        }




    }
}

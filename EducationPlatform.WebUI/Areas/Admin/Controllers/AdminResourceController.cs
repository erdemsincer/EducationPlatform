using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminResource")]
    public class AdminResourceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminResourceController(IHttpClientFactory httpClientFactory)
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
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await client.GetAsync("http://localhost:7028/api/Resource/GetResourceDetails");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultResourceDto>>(jsonData);
                return View(values);
            }

            TempData["Error"] = "Kaynaklar yüklenirken hata oluştu.";
            return View(new List<ResultResourceDto>());
        }

        [Route("RemoveResource/{id}")]
        public async Task<IActionResult> RemoveResource(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:7028/api/Resource/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Kategori silinirken hata oluştu!");
            return RedirectToAction("Index");
        }

    }
}

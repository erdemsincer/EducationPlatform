using EducationPlatform.Dto.UserDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminUser")]
    public class AdminUserController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminUserController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7028/api/User");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultUserDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultUserDto>());
        }
       

        [Route("RemoveUser/{id}")]
        public async Task<IActionResult> RemoveUser(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:7028/api/User/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Kategori silinirken hata oluştu!");
            return RedirectToAction("Index");
        }

    }
}

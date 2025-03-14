using EducationPlatform.Dto.MessageDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminMessage")]
    public class AdminMessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminMessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultMessageDto>>("http://localhost:7028/api/Message");
            return View(values);
        }

        
        [HttpGet]
        [Route("MessageDetail/{id}")]
        public async Task<IActionResult> MessageDetail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:7028/api/Message/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var message = JsonConvert.DeserializeObject<ResultMessageDto>(jsonData);
                return View(message);
            }

            return RedirectToAction("Index"); // Mesaj bulunamazsa listelemeye yönlendir
        }

        [HttpPost]
        [Route("RemoveMessage")]
        public async Task<IActionResult> RemoveMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:7028/api/Message/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Mesaj silinirken hata oluştu!");
            return RedirectToAction("Index");
        }
    }
}

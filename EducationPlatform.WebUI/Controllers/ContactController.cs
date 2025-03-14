using EducationPlatform.Dto.ContactDto;
using EducationPlatform.Dto.MessageDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultContactDto>>("http://localhost:7028/api/contact");
            ViewBag.map = values.Select(x => x.MapUrl).FirstOrDefault();
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto model)
        {
            var client = _httpClientFactory.CreateClient();
            await client.PostAsJsonAsync("http://localhost:7028/api/message", model);
            return NoContent();
        }
    }
}

using EducationPlatform.Dto.SubscriberDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.Controllers
{
    public class DiscussionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DiscussionController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(CreateSubscriberDto model)
        {
            var client = _httpClientFactory.CreateClient();
            await client.PostAsJsonAsync("https://localhost:7028/api/Subscriber", model);
            return NoContent();
        }
    }
}

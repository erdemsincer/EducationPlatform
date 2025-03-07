using EducationPlatform.Dto.DiscussionDto;
using EducationPlatform.Dto.SubscriberDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        public async Task<IActionResult> Detail(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7028/api/Discussion/GetDiscussionDetailWithReplies/{id}");

           

            var jsonData = await response.Content.ReadAsStringAsync();
            var discussionDetail = JsonConvert.DeserializeObject<DiscussionWithRepliesDto>(jsonData);

            return View(discussionDetail);
        }
    }
}

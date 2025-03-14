using EducationPlatform.Dto.AboutDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.Controllers
{
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultAboutDto>>("http://localhost:7028/api/About");
            return View(values);
        }
    }
}

using EducationPlatform.Dto.CategoryDto;
using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
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
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7028/api/Resource/GetResourceDetails");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultResourceDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultResourceDto>());
        }

    }
}

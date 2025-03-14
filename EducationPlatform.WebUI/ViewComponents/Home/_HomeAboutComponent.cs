using EducationPlatform.Dto.AboutDto;
using EducationPlatform.Dto.DiscussionDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Home
{
    public class _HomeAboutComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeAboutComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultAboutDto>>("http://localhost:7028/api/About");

            return View(values);
        }
    }
}

using EducationPlatform.Dto.BannerDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Home
{
    public class _HomeBannerComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeBannerComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task< IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultBannerDto>>("http://localhost:7028/api/Banner");

            return View(values);
        }
    }
}

using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Home
{
    public class _HomeResourceComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeResourceComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultResourceDto>>("http://localhost:7028/api/Resource/GetLatestResources");
            return View(values);
        }
    }
}

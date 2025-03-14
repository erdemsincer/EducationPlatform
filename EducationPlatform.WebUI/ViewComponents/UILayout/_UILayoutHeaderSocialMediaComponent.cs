using EducationPlatform.Dto.SocialMediaDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.UILayout
{
    public class _UILayoutHeaderSocialMediaComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UILayoutHeaderSocialMediaComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultSocialMediaDto>>("http://localhost:7028/api/SocialMedia");
            return View(values);
        }
    }
}

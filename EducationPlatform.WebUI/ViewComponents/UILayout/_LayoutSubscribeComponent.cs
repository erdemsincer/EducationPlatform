using EducationPlatform.Dto.ContactDto;
using EducationPlatform.Dto.SubscriberDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.UILayout
{
    public class _LayoutSubscribeComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _LayoutSubscribeComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultSubscriberDto>>("http://localhost:7028/api/Subscriber");
            return View(values);
        }
    }
}

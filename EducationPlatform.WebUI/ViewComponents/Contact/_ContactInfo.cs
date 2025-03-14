using EducationPlatform.Dto.ContactDto;
using EducationPlatform.Dto.SubscriberDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Contact
{
    public class _ContactInfo:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ContactInfo(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultContactDto>>("http://localhost:7028/api/contact");
            return View(values);
        }
    }
}

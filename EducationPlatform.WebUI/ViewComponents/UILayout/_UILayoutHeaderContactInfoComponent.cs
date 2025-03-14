using EducationPlatform.Dto.ContactDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.UILayout
{
    public class _UILayoutHeaderContactInfoComponent : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UILayoutHeaderContactInfoComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultContactDto>>("http://localhost:7028/api/Contact");
            return View(values);
        }
    }
}

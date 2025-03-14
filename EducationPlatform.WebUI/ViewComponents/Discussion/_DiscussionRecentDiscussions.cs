using EducationPlatform.Dto.DiscussionDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Discussion
{
    public class _DiscussionRecentDiscussions:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DiscussionRecentDiscussions(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultDiscussionDto>>("http://localhost:7028/api/Discussion/GetLastDiscussions");

            return View(values);
        }
    }
}

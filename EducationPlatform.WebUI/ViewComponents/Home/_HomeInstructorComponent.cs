using EducationPlatform.Dto.InstructorDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Home
{
    public class _HomeInstructorComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeInstructorComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultInstructorDto>>("http://localhost:7028/api/Instructor/last-four");
            return View(values);
        }
    }
}

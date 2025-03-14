using EducationPlatform.Dto.TestimonialDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Home
{
    public class _HomeTestimonialComponent:ViewComponent
    {
       private readonly IHttpClientFactory _httpClientFactory;

        public _HomeTestimonialComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultTestimonialDto>>("http://localhost:7028/api/Testimonial");
            return View(values);
        }
    }
}

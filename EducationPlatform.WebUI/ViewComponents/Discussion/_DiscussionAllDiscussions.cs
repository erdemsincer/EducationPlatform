﻿using EducationPlatform.Dto.DiscussionDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Discussion
{
    public class _DiscussionAllDiscussions:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _DiscussionAllDiscussions(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async  Task<IViewComponentResult >InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultDiscussionDto>>("https://localhost:7028/api/Discussion/GetAll");
            return View(values);  
        }
    }
}

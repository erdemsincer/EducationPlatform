﻿using EducationPlatform.Dto.CategoryDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.ViewComponents.Home
{
    public class _HomeCategoryComponent:ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _HomeCategoryComponent(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultCategoryDto>>("https://localhost:7028/api/Category");
            return View(values);
        }
    }
}

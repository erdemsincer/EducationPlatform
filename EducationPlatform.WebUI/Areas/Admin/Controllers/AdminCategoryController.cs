using EducationPlatform.Dto.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<ResultCategoryDto>> GetCategoriesAsync()
        {
           
        }
    }
    }
}

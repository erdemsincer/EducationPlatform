using EducationPlatform.Dto.CategoryDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminCategory")]
    public class AdminCategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:7028/api/Category");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData);
                return View(values);
            }
            return View(new List<ResultCategoryDto>());
        }

        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto createCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();

          
            Console.WriteLine("Gönderilen JSON: " + JsonConvert.SerializeObject(createCategoryDto));

            var responseMessage = await client.PostAsJsonAsync("http://localhost:7028/api/Category", createCategoryDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Kategori eklenirken hata oluştu!");
            return View(createCategoryDto);
        }

        [Route("RemoveCategory/{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:7028/api/Category/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Kategori silinirken hata oluştu!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:7028/api/Category/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<UpdateCategoryDto>(jsonData);
                return View(category);
            }
            return View(new UpdateCategoryDto()); 
        }

        [HttpPost]
        [Route("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var client = _httpClientFactory.CreateClient();

           
            Console.WriteLine("Güncellenen JSON: " + JsonConvert.SerializeObject(updateCategoryDto));

            var responseMessage = await client.PutAsJsonAsync($"http://localhost:7028/api/Category/{updateCategoryDto.Id}", updateCategoryDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Kategori güncellenirken hata oluştu!");
            return View(updateCategoryDto);
        }
    }
}

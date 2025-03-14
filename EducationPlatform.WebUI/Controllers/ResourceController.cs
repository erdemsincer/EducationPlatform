using EducationPlatform.Dto.ResourceDto;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ResourceController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult>Index()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultResourceDto>>("http://localhost:7028/api/Resource/GetResourceDetails");
            return View(values);
        }
        [HttpGet("Resources/Category/{id}")]
        public async Task<IActionResult> GetResourcesByCategoryId(int id)
        {
            var client = _httpClientFactory.CreateClient();

            try
            {
                // API'den yanıt al
                var response = await client.GetAsync($"http://localhost:7028/api/Resource/GetByCategory/{id}");

                // API başarılı bir yanıt döndürmüyorsa özel hata mesajı göster
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Category = "Kategori Bulunamadı!";
                    ViewBag.Message = "Bu kategoriye ait kaynak bulunamadı. Farklı bir kategori seçebilir veya yeni kaynak ekleyebilirsiniz.";
                    return View(new List<ResultResourceDto>());
                }

                // API 204 No Content döndürüyorsa, boş liste ile View'e yönlendir
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    ViewBag.Category = "Kategori Bulunamadı!";
                    ViewBag.Message = "Bu kategoriye ait herhangi bir kaynak mevcut değil.";
                    return View(new List<ResultResourceDto>());
                }

                // JSON verisini Model'e çevir
                var resources = await response.Content.ReadFromJsonAsync<List<ResultResourceDto>>();

                // Eğer kaynak bulunamazsa hata vermeden View'e boş liste gönder
                if (resources == null || resources.Count == 0)
                {
                    ViewBag.Category = "Kategori Bulunamadı!";
                    ViewBag.Message = "Bu kategoriye ait kaynak bulunamadı. Farklı bir kategori seçebilir veya yeni kaynak ekleyebilirsiniz.";
                    return View(new List<ResultResourceDto>());
                }

                // Kategori adını al
                ViewBag.Category = resources[0].CategoryName;

                return View(resources);
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Category = "Bağlantı Hatası!";
                ViewBag.Message = "API'ye bağlanırken hata oluştu. Lütfen daha sonra tekrar deneyiniz.";
                ViewBag.ErrorDetails = ex.Message;
                return View(new List<ResultResourceDto>());
            }
        }
    }
}

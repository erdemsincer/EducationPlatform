using EducationPlatform.Dto.BannerDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminBanner")]
    public class AdminBannerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBannerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task< IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultBannerDto>>("http://localhost:7028/api/Banner");
            return View(values);
        }

        [HttpGet]
        [Route("CreateBanner")]
        public IActionResult CreateBanner()
        {
            return View();
        }

    
        [HttpPost]
        [Route("CreateBanner")]
        public async Task<IActionResult> CreateBanner(CreateBannerDto createBannerDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                Console.WriteLine("Gönderilen JSON: " + JsonConvert.SerializeObject(createBannerDto));

                var responseMessage = await client.PostAsJsonAsync("http://localhost:7028/api/Banner", createBannerDto);
                string responseContent = await responseMessage.Content.ReadAsStringAsync();

                Console.WriteLine("API'den Gelen Yanıt: " + responseContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "API hatası: " + responseContent);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "İşlem sırasında hata oluştu: " + ex.Message);
            }

            return View(createBannerDto);
        }

        [Route("RemoveBanner/{id}")]
        public async Task<IActionResult> RemoveBanner(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:7028/api/Banner/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Banner silinirken hata oluştu!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UpdateBanner/{id}")]
        public async Task<IActionResult> UpdateBanner(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:7028/api/Banner/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var banner = JsonConvert.DeserializeObject<UpdateBannerDto>(jsonData);
                return View(banner);
            }
            return View(new UpdateBannerDto());
        }

        [HttpPost]
        [Route("UpdateBanner/{id}")]
        public async Task<IActionResult> UpdateBanner(UpdateBannerDto updateBannerDto)
        {
            var client = _httpClientFactory.CreateClient();


            Console.WriteLine("Güncellenen JSON: " + JsonConvert.SerializeObject(updateBannerDto));

            var responseMessage = await client.PutAsJsonAsync($"http://localhost:7028/api/Banner/", updateBannerDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Banner güncellenirken hata oluştu!");
            return View(updateBannerDto);
        }
    }
}

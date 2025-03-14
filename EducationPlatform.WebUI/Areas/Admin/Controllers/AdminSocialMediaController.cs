using EducationPlatform.Dto.SocialMediaDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminSocialMedia")]
    public class AdminSocialMediaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminSocialMediaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultSocialMediaDto>>("http://localhost:7028/api/SocialMedia");
            return View(values);
        }

        [HttpGet]
        [Route("CreateSocialMedia")]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateSocialMedia")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                Console.WriteLine("Gönderilen JSON: " + JsonConvert.SerializeObject(createSocialMediaDto));

                var responseMessage = await client.PostAsJsonAsync("http://localhost:7028/api/SocialMedia", createSocialMediaDto);
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

            return View(createSocialMediaDto);
        }

        [Route("RemoveSocialMedia")]
        public async Task<IActionResult> RemoveSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:7028/api/SocialMedia/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sosyal medya hesabı silinirken hata oluştu!");
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("UpdateSocialMedia/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:7028/api/SocialMedia/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var socialMedia = JsonConvert.DeserializeObject<UpdateSocialMediaDto>(jsonData);
                return View(socialMedia);
            }
            return View(new UpdateSocialMediaDto());
        }

        [HttpPost]
        [Route("UpdateSocialMedia/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var client = _httpClientFactory.CreateClient();
            Console.WriteLine("Güncellenen JSON: " + JsonConvert.SerializeObject(updateSocialMediaDto));

            var responseMessage = await client.PutAsJsonAsync($"http://localhost:7028/api/SocialMedia", updateSocialMediaDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Sosyal medya hesabı güncellenirken hata oluştu!");
            return View(updateSocialMediaDto);
        }
    }
}

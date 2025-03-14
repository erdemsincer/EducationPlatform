using EducationPlatform.Dto.ContactDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EducationPlatform.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminContact")]
    public class AdminContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var values = await client.GetFromJsonAsync<List<ResultContactDto>>("http://localhost:7028/api/Contact");
            return View(values);
        }

        [HttpGet]
        [Route("CreateContact")]
        public IActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateContact")]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                Console.WriteLine("Gönderilen JSON: " + JsonConvert.SerializeObject(createContactDto));

                var responseMessage = await client.PostAsJsonAsync("http://localhost:7028/api/Contact", createContactDto);
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

            return View(createContactDto);
        }
        [HttpPost] // POST olarak kullanmalısın, form ile gönderiyoruz!
        [Route("RemoveContact")]
        public async Task<IActionResult> RemoveContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"http://localhost:7028/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "İletişim bilgisi silinirken hata oluştu!");
            return RedirectToAction("Index");
        }


        [HttpGet]
        [Route("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContact(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"http://localhost:7028/api/Contact/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var contact = JsonConvert.DeserializeObject<UpdateContactDto>(jsonData);
                return View(contact);
            }
            return View(new UpdateContactDto());
        }

        [HttpPost]
        [Route("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            var client = _httpClientFactory.CreateClient();

            Console.WriteLine("Güncellenen JSON: " + JsonConvert.SerializeObject(updateContactDto));

            var responseMessage = await client.PutAsJsonAsync($"http://localhost:7028/api/Contact/", updateContactDto);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "İletişim bilgisi güncellenirken hata oluştu!");
            return View(updateContactDto);
        }
    }
}

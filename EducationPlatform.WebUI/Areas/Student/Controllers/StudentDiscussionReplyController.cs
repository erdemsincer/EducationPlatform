using EducationPlatform.Dto.DiscussionDto;
using EducationPlatform.Dto.DiscussionReplyDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace EducationPlatform.WebUI.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("Student/StudentDiscussionReply")]
    public class StudentDiscussionReplyController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentDiscussionReplyController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // ✅ Tartışmayı çek
            var discussionResponse = await client.GetAsync($"http://localhost:7028/api/Discussion/{id}");
            if (!discussionResponse.IsSuccessStatusCode)
            {
                TempData["Error"] = "Tartışma bilgisi alınamadı.";
                return RedirectToAction("Index", "StudentDiscussion");
            }

            var discussionData = await discussionResponse.Content.ReadAsStringAsync();
            var discussion = JsonConvert.DeserializeObject<ResultDiscussionDto>(discussionData);

            // ✅ Yorumları çek
            var repliesResponse = await client.GetAsync($"http://localhost:7028/api/DiscussionReply/GetReplies/{id}");
            var repliesData = await repliesResponse.Content.ReadAsStringAsync();
            var replies = JsonConvert.DeserializeObject<List<ResultDiscussionReplyDto>>(repliesData);

            ViewBag.Replies = replies;

            return View(discussion);
        }

        [HttpPost("AddReply")]
        public async Task<IActionResult> AddReply(CreateDiscussionReplyDto dto)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
            }

            dto.UserId = int.Parse(userId); // ✅ UserId ekle
          

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

            var jsonData = JsonConvert.SerializeObject(dto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:7028/api/DiscussionReply", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Yorum eklenemedi!";
                return RedirectToAction("Detail", "StudentDiscussion", new { area = "Student", id = dto.DiscussionId });
            }

            TempData["Success"] = "Yorum başarıyla eklendi!";
            return RedirectToAction("Detail", "StudentDiscussionReply", new { area = "Student", id = dto.DiscussionId });
        }
    }
}

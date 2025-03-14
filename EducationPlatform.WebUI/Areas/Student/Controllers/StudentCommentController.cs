using EducationPlatform.Dto.CommentDto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

[Area("Student")]
[Route("Student/StudentComment")]
public class StudentCommentController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public StudentCommentController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetString("UserId");
        if (string.IsNullOrEmpty(userId))
        {
            return RedirectToRoute(new { controller = "Auth", action = "Login", area = "" });
        }

        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

        var response = await client.GetAsync($"http://localhost:7028/api/Comment/User/{userId}");
        if (!response.IsSuccessStatusCode)
        {
            TempData["Error"] = "Yorumlar yüklenemedi.";
            return View(new List<ResultCommentDto>());
        }

        var jsonData = await response.Content.ReadAsStringAsync();
        var comments = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);

        return View(comments);
    }
    [Route("Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var client = _httpClientFactory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("AuthToken"));

        var response = await client.DeleteAsync($"http://localhost:7028/api/Comment/{id}");

        if (response.IsSuccessStatusCode)
        {
            TempData["Success"] = "✅ Yorum başarıyla silindi!";
        }
        else
        {
            TempData["Error"] = "❌ Yorum silinemedi.";
        }

        return RedirectToAction("Index");
    }


}

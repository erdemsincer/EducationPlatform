using EducationPlatform.Dto.DiscussionReplyDto;
using Microsoft.AspNetCore.Mvc;

public class _DiscussionReplyForm : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int discussionId)
    {
        var userId = HttpContext.Session.GetString("UserId");

        return View("Default", new CreateDiscussionReplyDto
        {
            DiscussionId = discussionId,
            UserId = userId != null ? int.Parse(userId) : 0, // Kullanıcı giriş yapmışsa ID al
            Message = ""
        });
    }
}

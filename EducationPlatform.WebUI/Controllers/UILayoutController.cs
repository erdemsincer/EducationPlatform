using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.Controllers
{
    public class UILayoutController : Controller
    {
        public IActionResult Layout()
        {
            return View();
        }
    }
}

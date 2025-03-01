using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.WebUI.Areas.Student.Controllers
{
    public class StudentLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult StudentHeaderPartial()
        {
            return PartialView();
        }

        public PartialViewResult StudentNavBarPartial()
        {
            return PartialView();
        }

        public PartialViewResult StudentSideBarPartial()
        {
            return PartialView();
        }
        public PartialViewResult StudentScriptPartial()
        {
            return PartialView();
        }
        public PartialViewResult StudentFooterPartial()
        {
            return PartialView();
        }
    }
}

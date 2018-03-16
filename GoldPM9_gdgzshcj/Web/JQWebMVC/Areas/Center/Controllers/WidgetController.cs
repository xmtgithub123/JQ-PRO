using System.Web.Mvc;

namespace JQ.BPM.Web.Areas.Center.Controllers
{
    public class WidgetController : JQ.Web.CoreController
    {
        // GET: System/Widget
        public ViewResult ChooseRole()
        {
            return View();
        }

        public ViewResult ChooseUser()
        {
            return View();
        }
    }
}
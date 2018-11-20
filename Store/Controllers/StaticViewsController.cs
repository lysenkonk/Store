using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    public class StaticViewsController : Controller
    {
        public ViewResult About()
        {
            return View("~/Views/Static/About.cshtml");
        }

        public ViewResult Delivery()
        {
            return View("~/Views/Static/Delivery.cshtml");
        }
    }
}

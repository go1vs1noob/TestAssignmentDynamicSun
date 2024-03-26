using Microsoft.AspNetCore.Mvc;

namespace AssignmentDynamicSun.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

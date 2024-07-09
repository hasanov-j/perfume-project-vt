using Microsoft.AspNetCore.Mvc;

namespace GR_30321.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

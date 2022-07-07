using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
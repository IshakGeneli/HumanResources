using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResources.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //return Redirect("Identity/Account/Login");
            //}
            return View();
        }

    }
}
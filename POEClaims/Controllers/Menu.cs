using Microsoft.AspNetCore.Mvc;

namespace POEClaim.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Privacy/Index.cshtml");
        }
    }
}
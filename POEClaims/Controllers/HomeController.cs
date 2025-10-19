using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POEClaim.Models;

namespace POEClaim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // ------------------- REGISTRATION SECTION -------------------

        [HttpGet]
        public IActionResult Index()
        {
            // Create table when first loading registration page
            sql_query creates = new sql_query();
            creates.create_table();

            return View();
        }

        [HttpPost]
        public IActionResult Index(register_user user)
        {
            if (ModelState.IsValid)
            {
                sql_query get_values = new sql_query();
                get_values.store_user(user.name, user.age);
            }

            return View(user);
        }

        // ------------------- LOGIN SECTION -------------------

        [HttpGet]
        public IActionResult Privacy(string? role)
        {
            // Optional: pass the role value to the view
            ViewBag.role = role;
            return View();
        }

        [HttpPost]
        public IActionResult Privacy(login Log)
        {
            if (ModelState.IsValid)
            {
                sql_query check = new sql_query();
                bool found = check.login_user(Log.name, Log.age);

                if (found)
                {
                    // Redirect to Welcome page after successful login
                    return RedirectToAction("Welcome", "Home");
                }
                else
                {
                    // Display an error message or stay on same view
                    ViewBag.LoginError = "User not found. Please check your credentials.";
                }
            }

            return View(Log);
        }

        // ------------------- GENERAL PAGES -------------------

        public IActionResult Welcome()
        {
            return View();
        }

        // ------------------- ROLE-BASED VIEWS -------------------

        public IActionResult Lecturer()
        {
            return View(); // Views/Home/Lecturer.cshtml
        }

        public IActionResult Project_Manager()
        {
            return View(); // Views/Home/Project_Manager.cshtml
        }

        public IActionResult Program_coordinator()
        {
            return View(); // Views/Home/Program_coordinator.cshtml
        }

        public IActionResult Claim()
        {
            return View(); // Views/Home/Claim.cshtml
        }

        public IActionResult Track_Claim()
        {
            return View(); // Views/Home/Track_Claim.cshtml
        }

        public IActionResult Pre_Approve()
        {
            return View(); // Views/Home/Pre_Approve.cshtml
        }

        public IActionResult ApproveClaimIndex()
        {
            return View(); // Views/Home/ApproveClaimIndex.cshtml
        }

        // ------------------- ERROR HANDLER -------------------

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

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
            all_queries creates = new all_queries();
            creates.creates_table();

            return View();
        }
[HttpPost]
public IActionResult Register(Register user)
{
    if (ModelState.IsValid)
    {
        if (string.IsNullOrWhiteSpace(user.name) ||
            string.IsNullOrWhiteSpace(user.surname) ||
            string.IsNullOrWhiteSpace(user.email) ||
            string.IsNullOrWhiteSpace(user.password))
        {
            ViewBag.RegisterError = "All fields are required.";
            return View("Index");
        }

        try
        {
            all_queries db = new all_queries();

            // Optional: check if user already exists
            bool exists = db.search_user(user.name, user.surname, user.email, user.password);
            if (exists)
            {
                ViewBag.RegisterError = "User already exists.";
                return View("Index");
            }

            db.store_user(user.name, user.surname, user.email, user.password);

            ViewBag.RegisterSuccess = "Account created successfully! You can now login.";
            return View("Index");
        }
        catch (Exception ex)
        {
            ViewBag.RegisterError = "Error creating account: " + ex.Message;
            return View("Index");
        }
    }

    return View("Index");
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
        public IActionResult Login(string Name, string Surname, string Email, string Password, string Role)
        {
            // Validate input
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ViewBag.LoginError = "All fields are required.";
                return View("Index");
            }

            // Use your all_queries class to check user
            all_queries check = new all_queries();
            bool userExists = check.search_user(Name, Surname, Email, Password);

            if (!userExists)
            {
                ViewBag.LoginError = "Invalid login credentials.";
                return View("Index");
            }

            // Redirect based on role
            switch (Role)
            {
                case "Lecturer":
                    HttpContext.Session.SetString("UserRole", "Lecturer"); // store role in session
                    HttpContext.Session.SetString("UserEmail", Email); // store their email
                    return RedirectToAction("Lecturer", "Home");

                case "Project_Manager":
                    HttpContext.Session.SetString("UserRole", "PC"); // store as PC
                    return RedirectToAction("Project_Manager", "Home");

                case "Program_coordinator":
                    HttpContext.Session.SetString("UserRole", "AM"); // store as AM
                    return RedirectToAction("Program_coordinator", "Home");

                case "HR":
                    HttpContext.Session.SetString("UserRole", "HR");
                    return RedirectToAction("Dashboard", "HR");


                default:
                    ViewBag.LoginError = "Invalid role selected.";
                    return View("Index");
            }
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
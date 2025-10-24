using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using POEClaim.Data;
using POEClaim.Models;
using Claim = POEClaim.Models.Claim;

namespace POEClaim.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Submit()
        {
            return View("Submit");
        }

        [HttpPost]
        public IActionResult Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {

                // Assigning the lecturer’s email from session
                claim.Email = HttpContext.Session.GetString("UserEmail") ?? "";

                // your file upload logic
                _context.Claims.Add(claim);
                _context.SaveChanges();
                return RedirectToAction("Success");
            }

            return View("Submit", claim);
        }

        public IActionResult Success()
        {
            return View("Success");
        }

        [HttpGet]
        public IActionResult Index()
        {
            all_queries queries = new all_queries();
            List<Claim> allClaims = queries.GetAllClaims();

            // Get the logged-in user role from session
            string userRole = HttpContext.Session.GetString("UserRole");

            ViewBag.UserRole = userRole; // pass role to the view for buttons

            // Filter claims based on role
            if (userRole == "Lecturer")
            {
                // Optionally filtering to show only the claims submitted by this lecturer
                // Assuming you have a way to identify lecturer, e.g., email or name in session
                string lecturerEmail = HttpContext.Session.GetString("UserEmail");
                allClaims = allClaims.Where(c => c.Email == lecturerEmail).ToList();
            }
            else if (userRole == "PC" || userRole == "AM")
            {
                // PC and AM see all claims, no filter
            }
            else
            {
                // Non-authorized user: show empty list
                allClaims = new List<Claim>();
            }

            return View(allClaims);
        }

        [HttpPost]
        public IActionResult VerifyClaim(int claimId)
        {
            var claim = _context.Claims.Find(claimId);
            if (claim != null)
            {
                claim.Status = "Verified"; // make sure you have a Status property in your Claim model
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RejectClaim(int claimId)
        {
            var claim = _context.Claims.Find(claimId);
            if (claim != null)
            {
                claim.Status = "Rejected"; // make sure you have a Status property in your Claim model
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ApproveClaim(int claimId)
        {
            var claim = _context.Claims.Find(claimId);
            if (claim != null)
            {
                claim.Status = "Approved"; // requires Status property in Claim model
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}

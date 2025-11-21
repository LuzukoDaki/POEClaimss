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

                // STRICT VALIDATION — Option A

                if (claim.Hours < 1 || claim.Hours > 40)
                {
                    ModelState.AddModelError("Hours", "Hours must be between 1 and 40.");
                }

                if (claim.Sessions < 1 || claim.Sessions > 20)
                {
                    ModelState.AddModelError("Sessions", "Sessions must be between 1 and 20.");
                }

                if (claim.Rate < 100 || claim.Rate > 500)
                {
                    ModelState.AddModelError("Rate", "Rate must be between 100 and 500.");
                }

                // If validation failed, return form
                if (!ModelState.IsValid)
                {
                    return View("Submit", claim);
                }


                // 1. Automatically calculate Backend TotalAmount
                claim.TotalAmount = (decimal)claim.Hours * claim.Rate;

                // 2. Attach lecturer email automatically
                claim.Email = HttpContext.Session.GetString("UserEmail") ?? "";

                // 3. Handle file upload (if a document was submitted)
                var file = Request.Form.Files["Document"];

                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine("wwwroot/uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    claim.DocumentPath = "/uploads/" + fileName;
                }

                // 4. Save claim
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
                //string lecturerEmail = HttpContext.Session.GetString("UserEmail");
                //allClaims = allClaims.Where(c => c.Email == lecturerEmail).ToList();
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

            if (claim == null)
                return RedirectToAction("Index");

            // AUTOMATED POLICY CHECKS (POE Requirement)
            List<string> errors = new List<string>();

            // 1. Hours
            if (claim.Hours < 1 || claim.Hours > 40)
                errors.Add("Hours must be between 1 and 40.");

            // 2. Sessions
            if (claim.Sessions < 1 || claim.Sessions > 20)
                errors.Add("Sessions must be between 1 and 20.");

            // 3. Rate
            if (claim.Rate < 100 || claim.Rate > 500)
                errors.Add("Rate must be between 100 and 500.");

            // 4. Email must exist
            if (string.IsNullOrEmpty(claim.Email))
                errors.Add("Lecturer email is missing.");

            // 5. TotalAmount must match calculation
            decimal expectedTotal = (decimal)claim.Hours * claim.Rate;
            if (claim.TotalAmount != expectedTotal)
                errors.Add("TotalAmount is incorrect.");

            // If ANY failures → DO NOT VERIFY
            if (errors.Count > 0)
            {
                TempData["VerificationErrors"] = string.Join(" | ", errors);
                return RedirectToAction("Index");
            }

            // AUTOMATION PASSED → VERIFY CLAIM
            claim.Status = "Verified";
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        //

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

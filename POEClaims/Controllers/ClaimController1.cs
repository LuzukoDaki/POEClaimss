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

    }
}

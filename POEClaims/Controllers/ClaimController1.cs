using Microsoft.AspNetCore.Mvc;
using POEClaim.Data;
using POEClaim.Models;

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
            return View();
        }

        [HttpPost]
        public IActionResult Submit(Claim claim)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (Request.Form.Files.Count > 0)
                {
                    var file = Request.Form.Files[0];
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    claim.DocumentPath = "/uploads/" + file.FileName;
                }

                _context.Claims.Add(claim);
                _context.SaveChanges();

                return RedirectToAction("Success");
            }

            return View(claim);
        }

        public IActionResult Success()
        {
            return View(); // Create Success.cshtml with a simple "Claim submitted" message
        }
    }
}

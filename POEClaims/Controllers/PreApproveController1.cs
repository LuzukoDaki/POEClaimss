using Microsoft.AspNetCore.Mvc;
using POEClaim.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace POEClaim.Controllers
{
    public class PreApproveController : Controller
    {
        // Mock data (replace with DB later)
        private static List<PreApproveClaim> claims = new List<PreApproveClaim>
        {
            new PreApproveClaim
            {
                Id = 1,
                Module = "502",
                Hours = 15,
                Rate = 150,
                Status = "Pending",
                SubmittedDate = DateTime.Parse("2025-09-01"),
                SubmittedTime = DateTime.Parse("2025-09-01 08:15"),
                Document = "claim1.pdf"
            },
            new PreApproveClaim
            {
                Id = 2,
                Module = "582",
                Hours = 12,
                Rate = 200,
                Status = "Pending",
                SubmittedDate = DateTime.Parse("2025-09-02"),
                SubmittedTime = DateTime.Parse("2025-09-02 10:00"),
                Document = "claim2.pdf"
            },
            new PreApproveClaim
            {
                Id = 3,
                Module = "590",
                Hours = 20,
                Rate = 180,
                Status = "Approved",
                SubmittedDate = DateTime.Parse("2025-09-08"),
                SubmittedTime = DateTime.Parse("2025-09-08 11:30"),
                PreApprovedDate = DateTime.Parse("2025-09-12"),
                Document = "claim3.pdf"
            }
        };

        public IActionResult Index()
        {
            return View(claims);
        }

        [HttpPost]
        public IActionResult PreApprove(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Approved";
                claim.PreApprovedDate = DateTime.Now;
                claim.PreApprovedTime = DateTime.Now;
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var claim = claims.FirstOrDefault(c => c.Id == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
            }
            return RedirectToAction("Index");
        }
    }
}
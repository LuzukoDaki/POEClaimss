/*using Microsoft.AspNetCore.Mvc;
using POEClaim.Models;
using System;
using System.Collections.Generic;

namespace POEClaim.Controllers
{
    public class ApproveClaimController : Controller
    {
        private static List<Claim> claims = new List<Claim>
        {
            new Claim
            {
                ClaimId = 1,
                ModuleId = "502",
                Sessions = 3,
                Hours = 15,
                Rate = 150,
                Status = "Pending",
                SubmittedDate = DateTime.Parse("2025-09-01"),
                SubmittedTime = "08:15",
                Document = "claim1.pdf"
            },
            new Claim
            {
                ClaimId = 2,
                ModuleId = "582",
                Sessions = 2,
                Hours = 12,
                Rate = 200,
                Status = "Pre-approved",
                SubmittedDate = DateTime.Parse("2025-09-02"),
                SubmittedTime = "10:00",
                PreApprovedDate = DateTime.Parse("2025-09-03"),
                PreApprovedTime = "09:45",
                Document = "claim2.pdf"
            },
            new Claim
            {
                ClaimId = 3,
                ModuleId = "590",
                Sessions = 5,
                Hours = 20,
                Rate = 180,
                Status = "Approved",
                SubmittedDate = DateTime.Parse("2025-09-05"),
                SubmittedTime = "11:30",
                PreApprovedDate = DateTime.Parse("2025-09-06"),
                PreApprovedTime = "14:10",
                FinalizedDate = DateTime.Parse("2025-09-07"),
                Document = "claim3.pdf"
            }
        };

        public IActionResult ApproveClaimIndex()
        {
            return View(claims);
        }
    }
} */
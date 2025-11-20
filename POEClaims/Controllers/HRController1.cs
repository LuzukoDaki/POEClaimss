using Microsoft.AspNetCore.Mvc;
using POEClaim.Models;
using System.Text;

namespace POEClaim.Controllers
{
    public class HRController : Controller
    {
        // Dashboard page
        public IActionResult Dashboard()
        {
            return View();
        }

        // Shows only Approved claims (uses your existing all_queries)
        public IActionResult ApprovedClaims()
        {
            all_queries q = new all_queries();
            var claims = q.GetAllClaims()
                          .Where(c => string.Equals(c.Status, "Approved", StringComparison.OrdinalIgnoreCase))
                          .ToList();

            return View(claims);
        }

        // Download Approved claims as CSV (opens in Excel)
        public FileResult DownloadApprovedClaimsCSV()
        {
            all_queries q = new all_queries();
            var claims = q.GetAllClaims()
                          .Where(c => string.Equals(c.Status, "Approved", StringComparison.OrdinalIgnoreCase))
                          .ToList();

            var sb = new StringBuilder();
            sb.AppendLine("ClaimId,Faculty,Module,Hours,Rate,TotalAmount,SubmissionDate,Email,Status");

            foreach (var c in claims)
            {
                // Escape commas in text fields by wrapping in quotes
                string faculty = c.FacultyName?.Replace("\"", "\"\"") ?? "";
                string module = c.ModuleName?.Replace("\"", "\"\"") ?? "";
                string document = c.DocumentPath?.Replace("\"", "\"\"") ?? "";
                sb.AppendLine($"\"{c.Id}\",\"{faculty}\",\"{module}\",\"{c.Hours}\",\"{c.Rate}\",\"{c.TotalAmount}\",\"{c.SubmissionDate}\",\"{c.Email}\",\"{c.Status}\"");
            }

            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());
            return File(bytes, "text/csv", "ApprovedClaimsReport.csv");
        }
    }
}

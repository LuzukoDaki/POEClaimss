using System;
using System.ComponentModel.DataAnnotations;

namespace POEClaim.Models
{
    public class Claim
    {
        [Key]
        public int ClaimID { get; set; }

        [Required]
        public string FacultyName { get; set; }

        [Required]
        public string ModuleName { get; set; }

        [Required]
        public int Sessions { get; set; }

        [Required]
        public double Hours { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public string DocumentPath { get; set; }   // Stores uploaded file path
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}

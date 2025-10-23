using System;
using System.ComponentModel.DataAnnotations;

namespace POEClaim.Models
{
    public class Claim
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FacultyName { get; set; } = string.Empty;

        [Required]
        public string ModuleName { get; set; } = string.Empty;

        [Required]
        public int Sessions { get; set; }

        [Required]
        public double Hours { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Rate { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        public string? DocumentPath { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;
    }
}

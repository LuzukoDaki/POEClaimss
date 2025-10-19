using System;

namespace POEClaim.Models
{
    public class PreApproveClaim
    {
        public int Id { get; set; }
        public string Module { get; set; }
        public int Hours { get; set; }
        public decimal Rate { get; set; }
        public string Status { get; set; }
        public DateTime SubmittedDate { get; set; }
        public DateTime SubmittedTime { get; set; }
        public DateTime? PreApprovedDate { get; set; }
        public DateTime? PreApprovedTime { get; set; }
        public string Document { get; set; }
    }
}
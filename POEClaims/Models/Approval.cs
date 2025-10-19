namespace POEClaim.Models
{
    public class Approval
    {
        public int ApprovalId { get; set; }
        public int ClaimId { get; set; }
        public string ApprovedBy { get; set; } = "";
        public DateTime ApprovalDate { get; set; } = DateTime.UtcNow;
        public bool Approved { get; set; }
        public string? Comments { get; set; }
    }
}
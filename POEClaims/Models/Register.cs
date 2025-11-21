using System.ComponentModel.DataAnnotations;

namespace POEClaim.Models
{
    public class Register
    {
        public string name { get; set; } // Changed from int to string
        public string surname { get; set; } //
        public string email { get; set; }
        public string password { get; set; }
    }
}
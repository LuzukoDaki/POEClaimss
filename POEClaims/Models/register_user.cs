using System.ComponentModel.DataAnnotations;

namespace POEClaim.Models
{
    public class register_user
    {
        [Required]
        public int age { get; set; }

        [Required]
        public string name { get; set; }
    }

}

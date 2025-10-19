using System.ComponentModel.DataAnnotations;

namespace POEClaim.Models
{

    public class login
    {
        [Required]
        public int age { get; set; }

        [Required]
        public string name { get; set; }
    }


}

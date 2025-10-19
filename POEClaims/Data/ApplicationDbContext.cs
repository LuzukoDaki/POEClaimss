using Microsoft.EntityFrameworkCore;
using POEClaim.Models;

namespace POEClaim.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Claim> Claims { get; set; }
    }
}

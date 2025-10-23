using Microsoft.EntityFrameworkCore;
using POEClaim.Models;
namespace POEClaim.Data;


//using System.Security.Claims;





public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Claim> Claims { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Set decimal precision to avoid EF Core warnings
        modelBuilder.Entity<Claim>()
            .Property(c => c.Rate)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Claim>()
            .Property(c => c.TotalAmount)
            .HasColumnType("decimal(18,2)");
    }
}

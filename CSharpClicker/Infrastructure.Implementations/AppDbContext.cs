using CSharpClicker.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Infrastructure.Implementations;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationUser>()
            .HasMany(u => u.UserBoosts)
            .WithOne()
            .HasForeignKey(ub => ub.UserId);
        modelBuilder.Entity<Boost>()
            .HasMany(b => b.UserBoosts)
            .WithOne()
            .HasForeignKey(ub => ub.BoostId);
        modelBuilder.Entity<UserBoost>()
            .HasNoKey();

        base.OnModelCreating(modelBuilder);
    }
}

using CSharpClicker.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Infrastructure.Implementations;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserBoost>()
            .HasOne(ub => ub.User)
            .WithMany(u => u.UserBoosts)
            .HasForeignKey(ub => ub.UserId);
        modelBuilder.Entity<UserBoost>()
            .HasOne(ub => ub.Boost)
            .WithMany(u => u.UserBoosts)
            .HasForeignKey(ub => ub.BoostId);
        modelBuilder.Entity<UserBoost>()
            .HasKey(ub => new { ub.UserId, ub.BoostId });

        base.OnModelCreating(modelBuilder);
    }
}

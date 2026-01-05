using CSharpClicker.Domain;
using CSharpClicker.Infrastructure.Abstractions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Infrastructure.Implementations;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Boost> Boosts { get; set; }

    public DbSet<UserBoost> UserBoosts { get; set; }

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

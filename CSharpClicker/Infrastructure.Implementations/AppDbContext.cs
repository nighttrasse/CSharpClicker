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

    public DbSet<Competition> Competitions { get; set; }

    public DbSet<CompetitionInvitation> CompetitionInvitations { get; set; }

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
        modelBuilder.Entity<CompetitionInvitation>()
            .HasOne(ci => ci.FromUser)
            .WithMany(u => u.CompetitionInvitations)
            .HasForeignKey(ci => ci.FromUserId);
        modelBuilder.Entity<CompetitionInvitation>()
            .HasOne(ci => ci.ToUser)
            .WithMany(u => u.CompetitionInvitations)
            .HasForeignKey(ci => ci.ToUserId);
        modelBuilder.Entity<Competition>()
            .HasOne(c => c.FirstUser)
            .WithMany(u => u.UserCompetitions)
            .HasForeignKey(c => c.FirstUserId);
        modelBuilder.Entity<Competition>()
            .HasOne(c => c.SecondUser)
            .WithMany(u => u.UserCompetitions)
            .HasForeignKey(c => c.SecondUserId);
        modelBuilder.Entity<UserBoost>()
            .HasKey(ub => new { ub.UserId, ub.BoostId });

        base.OnModelCreating(modelBuilder);
    }
}

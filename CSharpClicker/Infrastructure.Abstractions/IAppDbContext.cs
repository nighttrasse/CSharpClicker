using CSharpClicker.Domain;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.Infrastructure.Abstractions;

public interface IAppDbContext
{
    public DbSet<Boost> Boosts { get; set; }

    public DbSet<UserBoost> UserBoosts { get; set; }

    public DbSet<ApplicationUser> Users { get; set; }
}

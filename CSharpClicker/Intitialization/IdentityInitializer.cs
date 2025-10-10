using CSharpClicker.Domain;
using CSharpClicker.Infrastructure.Implementations;
using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Intitialization;

public static class IdentityInitializer
{
    public static void Initialize(IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<AppDbContext>();
        services.Configure<IdentityOptions>(c
            => c.Password.RequireNonAlphanumeric = false);
    }
}

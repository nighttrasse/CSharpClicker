using CSharpClicker.Infrastructure.Implementations;
using CSharpClicker.Intitialization;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    DbContextInitializer.InitializeDataBase(appDbContext);
}

app.MapDefaultControllerRoute();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    IdentityInitializer.Initialize(builder.Services);
    DbContextInitializer.InitializeDbContext(builder.Services);

    services.AddControllersWithViews();
}

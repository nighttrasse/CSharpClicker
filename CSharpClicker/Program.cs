using CSharpClicker.Infrastructure.Implementations;
using CSharpClicker.Intitialization;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    DbContextInitializer.InitializeDataBase(appDbContext);
}

app.UseAuthentication();

app.MapControllers();
app.MapDefaultControllerRoute();
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.Run();

void ConfigureServices(IServiceCollection services)
{
    IdentityInitializer.Initialize(builder.Services);
    DbContextInitializer.InitializeDbContext(builder.Services);

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);
    services.ConfigureApplicationCookie(opt =>
    {
        opt.LoginPath = "/auth/login";
        opt.LogoutPath = "/auth/logout";
    });

    services.AddMediatR(typeof(Program).Assembly);
    services.AddSwaggerGen();
    services.AddControllersWithViews();
}

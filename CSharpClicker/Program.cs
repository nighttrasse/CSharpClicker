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
app.Run();

void ConfigureServices(IServiceCollection services)
{
    IdentityInitializer.Initialize(builder.Services);
    DbContextInitializer.InitializeDbContext(builder.Services);

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(o =>
        {
            o.LoginPath = "/auth/login";
            o.LogoutPath = "/auth/logout";
        });

    services.AddMediatR(typeof(Program).Assembly);
    services.AddSwaggerGen();
    services.AddControllersWithViews();
}

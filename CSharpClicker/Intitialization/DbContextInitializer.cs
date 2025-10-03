using CSharpClicker.Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace CSharpClicker.Intitialization;

public static class DbContextInitializer
{
    public static void InitializeDbContext(IServiceCollection services)
    {
        var dbFilePath = GetPathToDatabaseFile();

        services.AddDbContext<AppDbContext>(o => o.UseSqlite($"Data Source={dbFilePath}"));
    }

    public static void InitializeDataBase(AppDbContext dbContext)
    {
        dbContext.Database.Migrate();
    }

    public static string GetPathToDatabaseFile()
    {
        var pathToLocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var dbFilePath = Path.Combine(pathToLocalApplicationData, "CSharpClicker", "CSharpClicker.db");

        return dbFilePath;
    }
}

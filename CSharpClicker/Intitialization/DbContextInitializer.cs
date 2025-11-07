using CSharpClicker.Domain;
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
        AddBoostsIfNotExist(dbContext);
    }

    private static void AddBoostsIfNotExist(AppDbContext dbContext)
    {
        var boosts = new Boost[]
        {
            new()
            {
                Title = "Стальная кирка",
                Price = 10,
                Profit = 1,
                IsAuto = false,
                Image = GetImageBytes("pickaxe.png"),
            },
            new()
            {
                Title = "Крестьянин",
                Price = 50,
                Profit = 1,
                IsAuto = true,
                Image = GetImageBytes("peasant.png"),
            },
            new()
            {
                Title = "Шахта",
                Price = 100,
                Profit = 10,
                IsAuto = false,
                Image = GetImageBytes("shaft.png"),
            },
            new()
            {
                Title = "Священник",
                Price = 500,
                Profit = 10,
                IsAuto = true,
                Image = GetImageBytes("priest.png"),
            },
            new()
            {
                Title = "Динамит",
                Price = 1000,
                Profit = 100,
                IsAuto = false,
                Image = GetImageBytes("explosion.png"),
            },
            new()
            {
                Title = "Слоник",
                Price = 5000,
                Profit = 100,
                IsAuto = true,
                Image = GetImageBytes("elephant.png"),
            },
            new()
            {
                Title = "Химическая обработка",
                Price = 10000,
                Profit = 1000,
                IsAuto = false,
                Image = GetImageBytes("chemistry.png"),
            },
        };

        foreach (var boost in boosts)
        {
            var exists = dbContext.Boosts.Any(b => b.Title == boost.Title);
            if (!exists)
            {
                dbContext.Boosts.Add(boost);
            }
        }

        dbContext.SaveChanges();

        static byte[] GetImageBytes(string imageName)
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "resources", imageName);
            return File.ReadAllBytes(filePath);
        }
    }

    public static string GetPathToDatabaseFile()
    {
        var pathToLocalApplicationData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        Directory.CreateDirectory(Path.Combine(pathToLocalApplicationData, "CSharpClicker"));

        var dbFilePath = Path.Combine(pathToLocalApplicationData, "CSharpClicker", "CSharpClicker.db");

        return dbFilePath;
    }
}

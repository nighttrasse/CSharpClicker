using CSharpClicker.DomainServices;
using CSharpClicker.Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.BackgroundServices;

public class AutoProfitService : BackgroundService
{
    private readonly IServiceProvider serviceProvider;

    public AutoProfitService(IServiceProvider services)
    {
        this.serviceProvider = services;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var serviceScope = serviceProvider.CreateScope();

            var appDbContext = serviceScope.ServiceProvider.GetRequiredService<IAppDbContext>();
            var connectedUsersRegistry = serviceScope.ServiceProvider.GetRequiredService<IConnectedUsersRegistry>();
            var scoreNotificationService = serviceScope.ServiceProvider.GetRequiredService<IScoreNotificationService>();

            var connectedUserIds = connectedUsersRegistry.GetAllConnectedUsers();
            var connectedUsers = await appDbContext.Users
                .Where(user => connectedUserIds.Contains(user.Id))
                .Include(user => user.UserBoosts)
                .ThenInclude(ub => ub.Boost)
                .ToListAsync(stoppingToken);

            foreach (var connectedUser in connectedUsers)
            {
                var profitPerSecond = connectedUser.UserBoosts.GetProfitPerSecond();

                connectedUser.CurrentScore += profitPerSecond;
                connectedUser.RecordScore += profitPerSecond;
            }

            await appDbContext.SaveChangesAsync(stoppingToken);

            foreach (var connectedUser in connectedUsers)
            {
                await scoreNotificationService.NotifyScoreChangedAsync(connectedUser.Id, connectedUser.CurrentScore,
                    connectedUser.RecordScore, stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}

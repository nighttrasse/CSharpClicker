using CSharpClicker.Infrastructure.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace CSharpClicker.Hubs;

public class ScoreNotificationService : IScoreNotificationService
{
    private readonly IHubContext<ClickerHub> hubContext;

    public ScoreNotificationService(IHubContext<ClickerHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    public Task NotifyBoostChangedAsync(Guid userId, int boostId, int quantity, long currentPrice, CancellationToken cancellationToken = default)
        => hubContext.Clients.User(userId.ToString()).SendAsync("BoostUpdated", boostId, quantity, currentPrice, cancellationToken);

    public Task NotifyProfitChangedAsync(Guid userId, long profitPerClick, long profitPerSecond, CancellationToken cancellationToken = default)
        => hubContext.Clients.User(userId.ToString()).SendAsync("ProfitUpdated", profitPerClick, profitPerSecond, cancellationToken);

    public Task NotifyScoreChangedAsync(Guid userId, long current, long record, CancellationToken cancellationToken = default)
        => hubContext.Clients.User(userId.ToString()).SendAsync("ScoreUpdated", current, record, cancellationToken);
}

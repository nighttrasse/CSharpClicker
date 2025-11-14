namespace CSharpClicker.Infrastructure.Abstractions;

public interface IScoreNotificationService
{
    Task NotifyScoreChangedAsync(Guid userId, long current, long record, CancellationToken cancellationToken = default);

    Task NotifyBoostChangedAsync(Guid userId, int boostId, int quantity, long currentPrice, CancellationToken cancellationToken = default);

    Task NotifyProfitChangedAsync(Guid userId, long profitPerClick, long profitPerSecond, CancellationToken cancellationToken = default);
}

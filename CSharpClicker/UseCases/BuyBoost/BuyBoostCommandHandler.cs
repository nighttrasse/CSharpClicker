using CSharpClicker.DomainServices;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.BuyBoost;

public class BuyBoostCommandHandler : IRequestHandler<BuyBoostCommand, Unit>
{
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;
    private readonly IAppDbContext appDbContext;
    private readonly IScoreNotificationService scoreNotificationService;

    public BuyBoostCommandHandler(
        ICurrentUserIdAccessor currentUserIdAccessor,
        IAppDbContext appDbContext,
        IScoreNotificationService scoreNotificationService)
    {
        this.currentUserIdAccessor = currentUserIdAccessor;
        this.appDbContext = appDbContext;
        this.scoreNotificationService = scoreNotificationService;
    }

    public async Task<Unit> Handle(BuyBoostCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserIdAccessor.GetCurrentUserId();
        var currentUser = await appDbContext.Users
            .Include(u => u.UserBoosts)
            .ThenInclude(ub => ub.Boost)
            .FirstAsync(u => u.Id == currentUserId, cancellationToken);

        var boost = await appDbContext.Boosts
            .FirstAsync(b => b.Id == request.BoostId, cancellationToken);

        var userBoost = currentUser.UserBoosts.FirstOrDefault(ub => ub.BoostId == boost.Id);

        long priceToPay;
        if (userBoost != null)
        {
            userBoost.Quantity++;
            priceToPay = userBoost.CurrentPrice;
            userBoost.CurrentPrice = (long)(userBoost.CurrentPrice * BoostConstants.ProfitPerClickMultiplier);
        }
        else
        {
            priceToPay = boost.Price;
            userBoost = new Domain.UserBoost
            {
                UserId = currentUser.Id,
                BoostId = boost.Id,
                CurrentPrice = (long)(boost.Price * BoostConstants.ProfitPerClickMultiplier),
                Quantity = 1,
            };
            currentUser.UserBoosts.Add(userBoost);
        }

        currentUser.CurrentScore -= priceToPay;

        await appDbContext.SaveChangesAsync(cancellationToken);

        await scoreNotificationService.NotifyScoreChangedAsync(currentUser.Id, currentUser.CurrentScore, currentUser.RecordScore, cancellationToken);
        await scoreNotificationService.NotifyProfitChangedAsync(
            currentUser.Id,
            currentUser.UserBoosts.GetProfitPerClick(),
            currentUser.UserBoosts.GetProfitPerSecond(),
            cancellationToken);
        await scoreNotificationService.NotifyBoostChangedAsync(
            currentUser.Id,
            boost.Id,
            userBoost.Quantity!,
            userBoost.CurrentPrice!,
            cancellationToken);

        return Unit.Value;
    }
}
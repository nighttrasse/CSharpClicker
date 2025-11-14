using CSharpClicker.DomainServices;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CSharpClicker.UseCases.RegisterClicks;

public class RegisterClickCommandHandler : IRequestHandler<RegisterClicksCommand, Unit>
{
    private readonly IAppDbContext appDbContext;
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;
    private readonly IScoreNotificationService scoreNotificationService;

    public RegisterClickCommandHandler(IAppDbContext appDbContext, 
        ICurrentUserIdAccessor currentUserIdAccessor,
        IScoreNotificationService scoreNotificationService)
    {
        this.appDbContext = appDbContext;
        this.currentUserIdAccessor = currentUserIdAccessor;
        this.scoreNotificationService = scoreNotificationService;
    }

    public async Task<Unit> Handle(RegisterClicksCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserIdAccessor.GetCurrentUserId();
        var currentUser = await appDbContext.Users
            .Include(u => u.UserBoosts)
            .FirstAsync(u => u.Id == currentUserId);
        
        var scoreToAdd = request.ClickCount * currentUser.UserBoosts.GetProfitPerClick();

        currentUser.CurrentScore += scoreToAdd;
        currentUser.RecordScore += scoreToAdd;

        await appDbContext.SaveChangesAsync(cancellationToken);
        await scoreNotificationService.NotifyScoreChangedAsync(currentUserId!.Value, currentUser.CurrentScore, currentUser.RecordScore, cancellationToken);

        return Unit.Value;
    }
}

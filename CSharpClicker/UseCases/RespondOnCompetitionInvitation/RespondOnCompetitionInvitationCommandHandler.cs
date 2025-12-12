using CSharpClicker.Domain;
using CSharpClicker.Infrastructure.Abstractions;
using MediatR;

namespace CSharpClicker.UseCases.RespondOnCompetitionInvitation;

public class RespondOnCompetitionInvitationCommandHandler : IRequestHandler<RespondOnCompetitionInvitationCommand, Unit>
{
    private readonly ICurrentUserIdAccessor currentUserIdAccessor;
    private readonly IAppDbContext appDbContext;

    public async Task<Unit> Handle(RespondOnCompetitionInvitationCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = currentUserIdAccessor.GetCurrentUserId() 
            ?? throw new InvalidOperationException("Current user ID is not available.");

        var competitionInvitation = appDbContext.CompetitionInvitations
            .FirstOrDefault(ci => ci.Id == request.InvitationId && ci.ToUserId == currentUserId)
                ?? throw new InvalidOperationException("Cannot find invitation.");

        competitionInvitation.IsAccepted = request.IsAccepted;

        var competition = new Competition
        {
            FirstUserId = competitionInvitation.FromUserId,
            SecondUserId = competitionInvitation.ToUserId,
            StartTime = DateTime.UtcNow
        };

        await appDbContext.Competitions.AddAsync(competition, cancellationToken);
        await appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

using MediatR;

namespace CSharpClicker.UseCases.RespondOnCompetitionInvitation;

public record RespondOnCompetitionInvitationCommand(Guid InvitationId, bool IsAccepted) : IRequest<Unit>;

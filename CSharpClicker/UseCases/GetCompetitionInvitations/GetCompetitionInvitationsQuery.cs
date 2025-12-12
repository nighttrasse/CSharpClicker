using CSharpClicker.Dtos;
using MediatR;

namespace CSharpClicker.UseCases.GetCompetitionInvitations;

public record GetCompetitionInvitationsQuery : IRequest<IEnumerable<CompetitionInvitationDto>>;

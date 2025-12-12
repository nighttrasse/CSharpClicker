using CSharpClicker.Dtos;
using MediatR;

namespace CSharpClicker.UseCases.SendCompetitionInvitation;

public record SendCompetitionInvitationCommand(Guid ToUserId) : IRequest<CompetitionInvitationDto>;

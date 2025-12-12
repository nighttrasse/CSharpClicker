using CSharpClicker.Dtos;

namespace CSharpClicker.ViewModels;

public record CompetitionsViewModel
{
    public Guid CurrentUserId { get; init; }

    public IEnumerable<CompetitionDto> Competitions { get; init; }
        = Enumerable.Empty<CompetitionDto>();

    public IEnumerable<CompetitionInvitationDto> CompetitionInvitations { get; init; }
        = Enumerable.Empty<CompetitionInvitationDto>();
}

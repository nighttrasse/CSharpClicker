using Microsoft.AspNetCore.Identity;

namespace CSharpClicker.Domain;

public class ApplicationUser : IdentityUser<Guid>
{
    public long CurrentScore { get; set; }

    public long RecordScore { get; set; }

    public ICollection<UserBoost> UserBoosts { get; set; }

    public ICollection<Competition> UserCompetitions { get; set; }

    public ICollection<CompetitionInvitation> CompetitionInvitations { get; set; }
}

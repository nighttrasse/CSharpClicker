namespace CSharpClicker.Domain;

public class CompetitionInvitation
{
    public Guid Id { get; set; }

    public Guid FromUserId { get; set; }

    public ApplicationUser FromUser { get; set; }

    public Guid ToUserId { get; set; }

    public ApplicationUser ToUser { get; set; }

    public DateTime SentAt { get; set; }

    public bool? IsAccepted { get; set; }
}

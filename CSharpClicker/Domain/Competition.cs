namespace CSharpClicker.Domain;

public class Competition
{
    public Guid Id { get; set; }

    public Guid FirstUserId { get; set; }

    public ApplicationUser FirstUser { get; set; }

    public Guid SecondUserId { get; set; }

    public ApplicationUser SecondUser { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int FirstUserScore { get; set; }

    public int SecondUserScore { get; set; }
}

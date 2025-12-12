namespace CSharpClicker.Dtos;

public class CompetitionDto
{
    public Guid Id { get; init; }

    public ShortUserInfoDto FirstUser { get; init; }

    public ShortUserInfoDto SecondUser { get; init; }

    public int FirstUserScore { get; init; }

    public int SecondUserScore { get; init; }

    public DateTime StartTime { get; init; }

    public DateTime? EndTime { get; init; }
}

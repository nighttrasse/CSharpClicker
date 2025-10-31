namespace CSharpClicker.Dtos;

public class UserInfoDto
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

	public long CurrentScore { get; set; }

	public long RecordScore { get; set; }

	public long ProfitPerClick { get; set; }

    public long ProfitPerSecond { get; set; }

    public IEnumerable<UserBoostDto> UserBoosts { get; set; } = Enumerable.Empty<UserBoostDto>();
}

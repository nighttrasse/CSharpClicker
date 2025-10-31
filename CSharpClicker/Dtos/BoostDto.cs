namespace CSharpClicker.Dtos;

public class BoostDto
{
	public int Id { get; init; }

	public string Name { get; init; }

	public long Price { get; set; }

	public long Profit { get; set; }

	public bool IsAuto { get; set; }

	public byte[] Image { get; init; }
}

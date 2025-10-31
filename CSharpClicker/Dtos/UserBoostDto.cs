namespace CSharpClicker.Dtos;

public class UserBoostDto
{
    public int BoostId { get; init; }

    public Guid UserId { get; init; }

    public int Quantity { get; init; }

    public long CurrentPrice { get; init; }
}

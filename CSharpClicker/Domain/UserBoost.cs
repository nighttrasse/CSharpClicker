namespace CSharpClicker.Domain;

public class UserBoost
{
    public Guid UserId { get; set; }

    public int BoostId { get; set; }

    public long CurrentPrice { get; set; }

    public int Quantity { get; set; }

    public Boost Boost { get; set; }

    public ApplicationUser User {get; set;}
}

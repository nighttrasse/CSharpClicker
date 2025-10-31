using CSharpClicker.Domain;

namespace CSharpClicker.DomainServices;

public static class UserBoostsExtensions
{
    public static long GetProfitPerClick(this ICollection<UserBoost> userBoosts)
        => 1 + userBoosts
            .Where(ub => !ub.Boost.IsAuto)
            .Select(ub => ub.Boost.Profit * ub.Quantity).Sum();

    public static long GetProfitPerSecond(this ICollection<UserBoost> userBoosts)
        => 1 + userBoosts
            .Where(ub => ub.Boost.IsAuto)
            .Select(ub => ub.Boost.Profit * ub.Quantity).Sum();
}

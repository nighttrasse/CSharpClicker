using CSharpClicker.Dtos;

namespace CSharpClicker.ViewModels;

public class IndexViewModel
{
	public UserInfoDto UserInfo { get; init; }

	public IEnumerable<BoostDto> Boosts { get; init; }
}

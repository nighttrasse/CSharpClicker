namespace CSharpClicker.Dtos;

public class CompetitionInvitationDto
{
    public Guid Id { get; set; }

    public UserInfoDto FromUser { get; set; }

    public UserInfoDto ToUser { get; set; }

    public bool? IsAccepted { get; set; }
}

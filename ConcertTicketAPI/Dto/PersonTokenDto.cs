namespace ConcertTicketAPI.Dto;

public class PersonTokenDto
{
    public Guid Guid { get; set; }
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;


    public DateTime AccessTokenExpire { get; set; }
}
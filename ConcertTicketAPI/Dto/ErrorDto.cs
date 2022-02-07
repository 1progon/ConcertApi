namespace ConcertTicketAPI.Dto;

public class ErrorDto
{
    public string Error { get; set; } = null!;
    public int? Code { get; set; }
}
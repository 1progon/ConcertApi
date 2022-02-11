using ConcertTicketAPI.Models.Enums;

namespace ConcertTicketAPI.Dto;

public class PersonShortDto : PersonTokenDto
{
    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public PersonType Type { get; set; }
}
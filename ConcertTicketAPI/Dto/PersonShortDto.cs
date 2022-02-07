using ConcertTicketAPI.Models.Enums;

namespace ConcertTicketAPI.Dto;

public class PersonShortDto
{
    public Guid Guid { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public PersonType Type { get; set; }

    public string? Token { get; set; }
    public DateTime? TokenExpire { get; set; }
}
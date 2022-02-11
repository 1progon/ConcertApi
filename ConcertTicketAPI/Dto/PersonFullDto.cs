using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Event;
using ConcertTicketAPI.Models.Location;
using ConcertTicketAPI.Models.Performer;
using ConcertTicketAPI.Models.Person;
using ConcertTicketAPI.Models.Venue;

namespace ConcertTicketAPI.Dto;

public class PersonFullDto : PersonShortDto
{
    public string? MiddleName { get; set; }

    private string? _zipCode;


    public string? Phone { get; set; } = null!;

    public string? Avatar { get; set; }
    public string? About { get; set; }

    // Location
    public int? CountryId { get; set; }
    public LocationCountry? Country { get; set; }

    public int? StateId { get; set; }
    public LocationState? State { get; set; }

    public int? CityId { get; set; }
    public LocationCity? City { get; set; }

    // Address

    public string? ZipCode
    {
        get => _zipCode ?? PostalCode;
        set => _zipCode = value;
    }

    public string? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? HouseLetter { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }


    public GenderType Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Photo { get; set; }

    public List<PersonCompanies>? Companies { get; set; }
    public List<Event>? Events { get; set; }
    public List<Venue>? Venues { get; set; }

    public List<Event>? EventFavorites { get; set; }
    public List<Event>? EventFollowings { get; set; }

    public List<Performer>? PerformerFavorites { get; set; }
    public List<Performer>? PerformerFollowings { get; set; }
}
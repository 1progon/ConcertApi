using System.ComponentModel.DataAnnotations.Schema;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Location;
using ConcertTicketAPI.Models.Person;
using Microsoft.Build.Framework;
using NpgsqlTypes;

namespace ConcertTicketAPI.Models.Venue;

public class Venue : BaseModel
{
    private string? _zipCode;
    [Required] public int CountryId { get; set; }
    [Required] public LocationCountry Country { get; set; } = null!;

    [Required] public int? StateId { get; set; }
    [Required] public LocationState? State { get; set; }

    [Required] public int CityId { get; set; }
    [Required] public LocationCity City { get; set; } = null!;

    public int? PersonOwnerId { get; set; }
    public Person.Person? PersonOwner { get; set; }
    
    public int? CompanyOwnerId { get; set; }
    public PersonCompanies? CompanyOwner { get; set; }

    public string? Description { get; set; }
    public string? Article { get; set; }

    public string? Image { get; set; }

    [Column(TypeName = "varchar(5)")]
    public string? ZipCode
    {
        get => _zipCode ?? PostalCode;
        set => _zipCode = value;
    }

    [Column(TypeName = "varchar(10)")] public string? PostalCode { get; set; }

    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? HouseLetter { get; set; }

    public NpgsqlPoint? Coordinates { get; set; }

    public string? Capacity { get; set; }
    public float? SquareSize { get; set; }

    public bool NearWater { get; set; }

    public DateOnly? Opened { get; set; }

    public string? Phone1 { get; set; }
    public string? Phone2 { get; set; }
    public string? Phone3 { get; set; }
    public string? OtherPhones { get; set; }

    public List<VenueWorkTime>? WorkTime { get; set; }

    public List<Event.Event>? Events { get; set; }
    public List<VenueParking>? Parking { get; set; }

    [Required] public VenueType Type { get; set; }

    public List<VenueImages>? Images { get; set; }
    public List<VenueZones>? Zones { get; set; }
}
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Location;

public class LocationState : BaseModel
{
    private string? _shortName;
    private string? _abbreviation;
    public string? Description { get; set; }

    [Column(TypeName = "varchar(6)")]
    public string? Abbreviation
    {
        get => _abbreviation ?? ShortName;
        set => _abbreviation = value;
    }

    public string? ShortName
    {
        get => _shortName ?? Name;
        set => _shortName = value;
    }

    [Required] public int CountryId { get; set; }
    [Required] public LocationCountry Country { get; set; } = null!;

    public List<LocationCity>? Cities { get; set; }
}
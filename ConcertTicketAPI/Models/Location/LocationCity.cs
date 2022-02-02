using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Location;

public class LocationCity : BaseModel
{
    private string? _shortName;


    public bool IsStateCapital { get; set; }
    public bool IsCountryCapital { get; set; }

    [Required] public int? StateId { get; set; }
    [Required] public LocationState? State { get; set; } = null!;


    public string? Description { get; set; }

    public List<Event.Event>? Events { get; set; }
    public List<Venue.Venue>? Venues { get; set; }

    public string? Iata { get; set; }

    public string? ShortName
    {
        get => _shortName ?? Name;
        set => _shortName = value;
    }

    public bool Popular { get; set; }

    public TimeZone? TimeZone { get; set; }
}
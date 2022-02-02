using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ConcertTicketAPI.Models.Phone;

namespace ConcertTicketAPI.Models.Location;

public class LocationCountry : BaseModel
{
    private string? _shortName;
    public string? Description { get; set; }

    public List<LocationState>? States { get; set; }

    public List<Event.Event>? Events { get; set; }


    [Column(TypeName = "char(2)")] public string? Iso2Code { get; set; }

    public string? ShortName
    {
        get => _shortName ?? Name;
        set => _shortName = value;
    }

    public bool Popular { get; set; }

    public CountryPhoneCode? PhoneCode { get; set; }
}
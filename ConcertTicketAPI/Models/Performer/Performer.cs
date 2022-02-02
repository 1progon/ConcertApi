using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Middle;
using ConcertTicketAPI.Models.Venue;

namespace ConcertTicketAPI.Models.Performer;

public class Performer : BaseModel
{
    public string? Description { get; set; }
    public string? Article { get; set; }

    public string? Image { get; set; }

    public List<Event.Event>? Events { get; set; }


    public List<PerformerImages>? Images { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PerformerType Type { get; set; }

    [Required] public int SubCategoryId { get; set; }
    [Required] public PerformerSubCategory SubCategory { get; set; } = null!;

    public List<VenueTicket>? Tickets { get; set; }
    public List<VenueParking>? Parking { get; set; }

    public bool Popular { get; set; }

    public DateOnly? BirthDate { get; set; }

    public long? Likes { get; set; }

    public List<Performer>? Members { get; set; }

    public int? ParentId { get; set; }
    public Performer? Parent { get; set; }

    public List<Person.Person>? PersonFavorites { get; set; }
    public List<Person.Person>? PersonFollowers { get; set; }


    // Many to many table
    public List<PersonPerformerFavoritesMiddle>? PersonPerformerFavorites { get; set; }
    public List<PersonPerformerFollowingMiddle>? PersonPerformerFollowings { get; set; }
    public List<EventPerformerMiddle>? EventPerformers { get; set; }
}
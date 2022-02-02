using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Middle;
using ConcertTicketAPI.Models.Person;
using ConcertTicketAPI.Models.Venue;
using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Event;

public class Event : BaseModel
{
    public int? PersonId { get; set; }
    public Person.Person? Person { get; set; }

    public int? CompanyId { get; set; }
    public PersonCompanies? Company { get; set; }

    public string? Description { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }

    public List<Performer.Performer>? Performers { get; set; }


    [Required] public bool Active { get; set; }
    [Required] public EventStatus Status { get; set; }


    [Required] public int VenueId { get; set; }
    [Required] public Venue.Venue Venue { get; set; } = null!;


    public List<VenueTicket>? Tickets { get; set; }
    public List<VenueParking>? Parking { get; set; }

    public List<EventImages>? Images { get; set; }

    [Required] public int SubCategoryId { get; set; }
    [Required] public EventSubCategory SubCategory { get; set; } = null!;

    public List<Person.Person>? PersonFavorites { get; set; }
    public List<Person.Person>? PersonFollowers { get; set; }


    public long? LikesCount { get; set; }

    public List<EventsTag>? Tags { get; set; }
    public List<EventsLike>? Likes { get; set; }


    // Many to many table
    public List<PersonEventFavoritesMiddle>? PersonEventFavorites { get; set; }
    public List<PersonEventFollowingsMiddle>? PersonEventFollowings { get; set; }
    public List<EventTagMiddle>? EventTagMiddles { get; set; }
    public List<EventPerformerMiddle>? EventPerformers { get; set; }
}
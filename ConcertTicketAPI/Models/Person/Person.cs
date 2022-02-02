using System.ComponentModel.DataAnnotations.Schema;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Middle;

namespace ConcertTicketAPI.Models.Person;

public class Person : AccountBase
{
    public override int Id { get; set; }
    [Column(TypeName = "varchar(255)")] public string? FirstName { get; set; }
    [Column(TypeName = "varchar(255)")] public string? LastName { get; set; }
    [Column(TypeName = "varchar(255)")] public string? MiddleName { get; set; }

    public PersonType Type { get; set; }
    [NotMapped] public bool IsAdmin => Type == PersonType.Admin;

    public GenderType Gender { get; set; }

    public DateOnly? BirthDate { get; set; }

    public string? Photo { get; set; }

    public List<PersonCompanies>? Companies { get; set; }
    public List<Event.Event>? Events { get; set; }
    public List<Venue.Venue>? Venues { get; set; }

    public List<Event.Event>? EventFavorites { get; set; }
    public List<Event.Event>? EventFollowings { get; set; }

    public List<Performer.Performer>? PerformerFavorites { get; set; }
    public List<Performer.Performer>? PerformerFollowings { get; set; }


    // Many to many table
    public List<PersonEventFavoritesMiddle>? PersonEventFavorites { get; set; }
    public List<PersonEventFollowingsMiddle>? PersonEventFollowings { get; set; }
    public List<PersonPerformerFavoritesMiddle>? PersonPerformerFavorites { get; set; }
    public List<PersonPerformerFollowingMiddle>? PersonPerformerFollowings { get; set; }
}
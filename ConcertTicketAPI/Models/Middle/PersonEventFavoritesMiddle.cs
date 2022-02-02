using System.ComponentModel.DataAnnotations;

namespace ConcertTicketAPI.Models.Middle;

public class PersonEventFavoritesMiddle
{
    [Required] public int PersonId { get; set; }
    [Required] public Person.Person Person { get; set; } = null!;
    
    [Required] public int EventId { get; set; }
    [Required] public Event.Event Event { get; set; } = null!;
}
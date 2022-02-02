using ConcertTicketAPI.Models.Enums;
using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Event;

public class EventsLike
{
    [Required] public int EventId { get; set; }
    [Required] public Event Event { get; set; } = null!;

    
    [Required] public int PersonId { get; set; }
    [Required] public Person.Person Person { get; set; } = null!;

    [Required] public LikesType Type { get; set; }
}
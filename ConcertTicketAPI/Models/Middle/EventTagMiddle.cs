using ConcertTicketAPI.Models.Event;
using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Middle;

public class EventTagMiddle
{
    [Required] public int TagId { get; set; }
    [Required] public EventsTag Tag { get; set; } = null!;
    
    [Required] public int EventId { get; set; }
    [Required] public Event.Event Event { get; set; } = null!;
}
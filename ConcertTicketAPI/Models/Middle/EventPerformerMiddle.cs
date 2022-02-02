using System.ComponentModel.DataAnnotations;

namespace ConcertTicketAPI.Models.Middle;

public class EventPerformerMiddle
{
    [Required] public int EventId { get; set; }
    [Required] public Event.Event Event { get; set; } = null!;

    [Required] public int PerformerId { get; set; }
    [Required] public Performer.Performer Performer { get; set; } = null!;
}
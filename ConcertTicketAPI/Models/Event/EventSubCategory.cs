using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Event;

public class EventSubCategory : BaseModel
{
    public string? Description { get; set; }

    [Required] public int CategoryId { get; set; }
    [Required] public EventCategory Category { get; set; } = null!;

    public List<Event>? Events { get; set; }
}
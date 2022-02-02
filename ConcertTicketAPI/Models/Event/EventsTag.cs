using System.ComponentModel.DataAnnotations.Schema;
using ConcertTicketAPI.Models.Middle;

namespace ConcertTicketAPI.Models.Event;

public class EventsTag : BaseModel
{
    [Column(TypeName = "varchar(255)")] public string? Description { get; set; }
    [Column(TypeName = "varchar(255)")] public string? Icon { get; set; }

    public List<Event>? Events { get; set; }
    
    // Many to many
    public List<EventTagMiddle>? EventTagMiddles { get; set; } 
}
using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Venue;

public class VenueParking : BaseModel
{
    public int? EventId { get; set; }
    public Event.Event? Event { get; set; }

    [Required] public int VenueId { get; set; }
    [Required] public Venue Venue { get; set; } = null!;


    public bool IsVip { get; set; }
    public bool IsRefundable { get; set; }

    [Required] public decimal Price { get; set; }
    public string? Zone { get; set; }
}
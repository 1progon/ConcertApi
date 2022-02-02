using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertTicketAPI.Models.Enums;

namespace ConcertTicketAPI.Models.Venue;

public class VenueTicket : BaseModel
{
    [Required] public int VenueZonesId { get; set; }
    [Required] public VenueZones VenueZones { get; set; } = null!;

    public int? EventId { get; set; }
    public Event.Event? Event { get; set; }

    public bool IsVip { get; set; }
    public bool IsRefundable { get; set; }

    public TicketStatus Status { get; set; }

    [Required] public decimal Price { get; set; }
    [Column(TypeName = "varchar(255)")] public string? Seat { get; set; }
}
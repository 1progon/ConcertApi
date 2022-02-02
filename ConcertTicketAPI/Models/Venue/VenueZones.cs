using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketAPI.Models.Venue;

public class VenueZones
{
    [Key] public int Id { get; set; }

    [Required] public int VenueId { get; set; }
    [Required] public Venue Venue { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    public int? ZoneId { get; set; }

    public int SeatsCount { get; set; }
    public int TicketsCount { get; set; }
    public decimal? TicketPrice { get; set; }
    

    public List<VenueTicket>? Tickets { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace ConcertTicketAPI.Models.Venue;

public class VenueImages
{
    [Key] public int Id { get; set; }
    [Required] public string Filename { get; set; } = null!;
    [Required] public string Folder { get; set; } = null!;

    [Required] public int VenueId { get; set; }
    [Required] public Venue Venue { get; set; } = null!;
}
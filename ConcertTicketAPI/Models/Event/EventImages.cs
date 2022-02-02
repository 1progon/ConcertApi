using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketAPI.Models.Event;

public class EventImages
{
    [Key] public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Filename { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Folder { get; set; } = null!;

    public string? MimeType { get; set; }

    [Required] public int EventId { get; set; }
    [Required] public Event Event { get; set; } = null!;
}
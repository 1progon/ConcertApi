using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketAPI.Models.Performer;

public class PerformerImages
{
    [Key] public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Filename { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Folder { get; set; } = null!;

    public string? MimeType { get; set; }

    [Required] public int PerformerId { get; set; }
    [Required] public Performer Performer { get; set; } = null!;
}
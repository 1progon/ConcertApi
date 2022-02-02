using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcertTicketAPI.Models.Person;

public class PersonCompaniesImages
{
    [Key] public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Filename { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Folder { get; set; } = null!;

    public string? MimeType { get; set; }

    [Required] public int PersonCompaniesId { get; set; }
    [Required] public PersonCompanies PersonCompanies { get; set; } = null!;
}
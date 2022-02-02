using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Models.Person;

public class PersonCompanies
{
    public int Id { get; set; }

    [Required] public int PersonId { get; set; }
    [Required] public Models.Person.Person Person { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Name { get; set; } = null!;

    [Column(TypeName = "varchar(255)")] public string? Type { get; set; } // LLC, LLP, LP, Corp ....

    public string? Image { get; set; }

    public List<PersonCompaniesImages>? Images { get; set; }

    public List<Event.Event>? Events { get; set; }
    public List<Venue.Venue>? Venues { get; set; }

    public long? Likes { get; set; }
}
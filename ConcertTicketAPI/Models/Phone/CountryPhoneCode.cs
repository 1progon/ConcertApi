using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertTicketAPI.Models.Location;

namespace ConcertTicketAPI.Models.Phone;

public class CountryPhoneCode
{
    [Key] public int Id { get; set; }

    [Microsoft.Build.Framework.Required]
    [Column(TypeName = "varchar(10)")]
    public string Code { get; set; } = null!;

    public string? Format { get; set; }

    [Required] public int CountryId { get; set; }
    [Required] public LocationCountry Country { get; set; } = null!;
}
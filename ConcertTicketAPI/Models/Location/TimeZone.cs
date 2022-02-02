using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;

namespace ConcertTicketAPI.Models.Location;

public class TimeZone : BaseModel
{
    [Required] public LocationCountry Country { get; set; } = null!;

    public NpgsqlPoint? Coordinates { get; set; }

    [Column(TypeName = "varchar(255)")] public string? Comments { get; set; }

    [Required]
    [Column(TypeName = "varchar(10)")]
    public string UtcOffset { get; set; } = null!;

    [Column(TypeName = "varchar(10)")] public string? UtcDstOffset { get; set; }

    public string? Notes { get; set; }
}
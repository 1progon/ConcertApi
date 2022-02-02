using System.ComponentModel.DataAnnotations;

namespace ConcertTicketAPI.Models.Performer;

public class PerformerSubCategory : BaseModel
{
    public string? Description { get; set; }

    [Required] public int CategoryId { get; set; }
    [Required] public PerformerCategory Category { get; set; } = null!;

    public List<Performer>? Performers { get; set; }
}
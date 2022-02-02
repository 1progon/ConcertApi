using System.ComponentModel.DataAnnotations;

namespace ConcertTicketAPI.Models.Middle;

public class PersonPerformerFollowingMiddle
{
    [Required] public int PersonId { get; set; }
    [Required] public Person.Person Person { get; set; } = null!;

    [Required] public int PerformerId { get; set; }
    [Required] public Performer.Performer Performer { get; set; } = null!;
}
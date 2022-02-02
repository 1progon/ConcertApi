using System.ComponentModel.DataAnnotations;

namespace ConcertTicketAPI.Models.Venue;

public class VenueWorkTime
{
    [Key] public int Id { get; set; }
    [Required] public DayOfWeek WeekDay { get; set; }
    [Required] public TimeSpan LunchBreak { get; set; }
    [Required] public TimeSpan WorkTime { get; set; }

    [Required] public int VenueId { get; set; }
    [Required] public Venue Venue { get; set; } = null!;
}
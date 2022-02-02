namespace ConcertTicketAPI.Models.Event;

public class EventCategory : BaseModel
{
    public override int Id { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    public bool InHeader { get; set; }

    public List<EventSubCategory>? SubCategories { get; set; }
}
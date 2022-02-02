namespace ConcertTicketAPI.Models.Performer;

public class PerformerCategory : BaseModel
{
    public override int Id { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    public bool InHeader { get; set; }

    public List<PerformerSubCategory>? SubCategories { get; set; }
}
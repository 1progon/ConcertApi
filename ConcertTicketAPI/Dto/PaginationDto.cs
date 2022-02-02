namespace ConcertTicketAPI.Dto;

public class PaginationDto
{
    public int Total { get; set; }

    public int PerPage { get; set; } = 20;
    public int PageId { get; set; } = 1;

    public int LastPage { get; set; } = 1;
}
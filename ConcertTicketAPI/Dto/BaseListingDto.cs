using ConcertTicketAPI.Models;

namespace ConcertTicketAPI.Dto;

public class BaseListingDto<T> where T : BaseModel
{
    public List<T> Items { get; set; } = null!;
    public PaginationDto Pagination { get; set; } = null!;
}
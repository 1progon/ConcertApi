using ConcertTicketAPI.Models.Location;
using ConcertTicketAPI.Models.Phone;

namespace ConcertTicketAPI.Areas.Admin.AdminViewModels;

public class RegisterViewModel
{
    public List<LocationCountry>? Countries { get; set; }
    public List<GenderTypeValues>? GenderTypes { get; set; }

    public IQueryable<object>? PhoneCodes { get; set; }
}

public struct GenderTypeValues
{
    public string Name { get; set; }
    public int Value { get; set; }
}
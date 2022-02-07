using ConcertTicketAPI.Dto;
using ConcertTicketAPI.Models.Location;

namespace ConcertTicketAPI.Areas.Admin.AdminDto;

public class RegisterGetDto
{
    public List<LocationCountry>? Countries { get; set; }
    public List<GenderTypeValues>? GenderTypes { get; set; }
    public IEnumerable<CountryPhoneCodeDto>? PhoneCodes { get; set; }
}
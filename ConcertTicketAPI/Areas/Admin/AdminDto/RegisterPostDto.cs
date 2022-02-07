using ConcertTicketAPI.Models.Enums;
using Microsoft.Build.Framework;

namespace ConcertTicketAPI.Areas.Admin.AdminDto;

public class RegisterPostDto
{
    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
    [Required] public string PasswordConfirm { get; set; } = null!;

    public int CountryId { get; set; }
    public int StateId { get; set; }
    public int CityId { get; set; }

    public GenderType Gender { get; set; }

    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;
    public string? MiddleName { get; set; } = null!;


    public DateOnly? BirthDate { get; set; }

    // public string  Photo {get; set;} = null!;
    // public string  Avatar {get; set;} = null!;

    public string? PhoneCode { get; set; }
    public string? Phone { get; set; }
    public string? About { get; set; }


    public string? ZipCode { get; set; }
    public string? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? HouseLetter { get; set; }
}
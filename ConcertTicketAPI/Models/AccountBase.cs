using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConcertTicketAPI.Models.Enums;
using ConcertTicketAPI.Models.Location;
using Microsoft.EntityFrameworkCore;

namespace ConcertTicketAPI.Models;

[Index(nameof(Email), IsUnique = true)]
[Index(nameof(Phone), IsUnique = true)]
public abstract class AccountBase
{
    private string? _zipCode;

    [Key] public virtual int Id { get; set; }
    public Guid Guid { get; set; }

    [Required] public bool Active { get; set; }
    [Required] public AccountStatus Status { get; set; } = AccountStatus.Moderation;

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(255)")]
    public string Password { get; set; } = null!;

    [Column(TypeName = "varchar(255)")] public string? Token { get; set; }


    public DateTime? TokenExpire { get; set; }

    [Column(TypeName = "varchar(255)")] public string? Phone { get; set; } = null!;

    public string? Avatar { get; set; }
    public string? About { get; set; }

    // Location
    public int? CountryId { get; set; }
    public LocationCountry? Country { get; set; }

    public int? StateId { get; set; }
    public LocationState? State { get; set; }

    public int? CityId { get; set; }
    public LocationCity? City { get; set; }

    // Address
    [Column(TypeName = "varchar(5)")]
    public string? ZipCode
    {
        get => _zipCode ?? PostalCode;
        set => _zipCode = value;
    }

    [Column(TypeName = "varchar(10)")] public string? PostalCode { get; set; }
    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? HouseLetter { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
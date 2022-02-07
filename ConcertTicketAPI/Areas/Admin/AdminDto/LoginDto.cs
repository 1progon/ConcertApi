using System.ComponentModel.DataAnnotations;

namespace ConcertTicketAPI.Areas.Admin.AdminDto;

public class LoginDto
{

    [Required] public string Email { get; set; } = null!;
    [Required] public string Password { get; set; } = null!;
}
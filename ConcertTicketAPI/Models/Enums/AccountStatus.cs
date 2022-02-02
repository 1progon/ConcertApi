using System.ComponentModel;

namespace ConcertTicketAPI.Models.Enums;

public enum AccountStatus
{
    Moderation = 0,
    Active = 1,
    Banned = 2,
    Archived = 3,
    
    [Description("Disabled By Admin")] DisabledByAdmin = 4,
    [Description("Disabled By User")] DisabledByUser = 5,
    
    [Description("Password Forgot")] PasswordForgot = 6,
    
    [Description("Deleted By Admin")] DeletedByAdmin = 7,
    [Description("Deleted By User")] DeletedByUser = 8,
}
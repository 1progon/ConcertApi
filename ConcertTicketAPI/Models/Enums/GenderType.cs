using System.ComponentModel;

namespace ConcertTicketAPI.Models.Enums;

public enum GenderType
{
    [Description("Not Known")] NotKnown = 0,
    // [Display(Description = "Not Known", Name = "Not Known")]
    Male = 1,
    Female = 2,

    [Description("Not Applicable")]
    // [Display(Description = "Not Applicable", Name = "Not Applicable")]
    NotApplicable = 9
}
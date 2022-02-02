using System.ComponentModel;

namespace ConcertTicketAPI.Models.Enums;

public enum EventStatus
{
    Scheduled = 0,
    Canceled = 1,
    [Description("In Progress")] InProgress = 2,
    Stopped = 3,
    Completed = 4,
    Closed = 5
}
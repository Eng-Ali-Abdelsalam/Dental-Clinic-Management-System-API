using System;

namespace DentalClinic.Core.Enums
{
    public enum AppointmentStatus
    {
        Scheduled,
        Confirmed,
        CheckedIn,
        InProgress,
        Completed,
        Canceled,
        NoShow,
        Rescheduled
    }
}

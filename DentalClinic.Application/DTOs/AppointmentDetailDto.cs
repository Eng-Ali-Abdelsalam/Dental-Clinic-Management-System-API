using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class AppointmentDetailDto : AppointmentDto
    {
        public int? RecurringIntervalDays { get; set; }
        public DateTime? ReminderSentAt { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string CancellationReason { get; set; }
        public PatientDto Patient { get; set; }
        public DentistDto Dentist { get; set; }
        public TreatmentDto Treatment { get; set; }
    }
}

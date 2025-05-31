using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid DentistId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public AppointmentStatus Status { get; set; }
        public AppointmentType Type { get; set; }
        public Guid? TreatmentId { get; set; }
        public bool IsRescheduled { get; set; }
        public bool IsRecurring { get; set; }
        public int? RecurringIntervalDays { get; set; }
        public DateTime? ReminderSentAt { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string CancellationReason { get; set; }

        // Navigation Properties
        public Patient Patient { get; set; }
        public Dentist Dentist { get; set; }
        public Treatment Treatment { get; set; }
        public Room Room { get; set; }
        public Guid? RoomId { get; set; }

        public TimeSpan Duration => EndTime - StartTime;
    }
}

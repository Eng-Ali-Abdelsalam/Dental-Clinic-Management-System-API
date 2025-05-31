using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class CreateAppointmentDto
    {
        public Guid PatientId { get; set; }
        public Guid DentistId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public AppointmentType Type { get; set; }
        public Guid? TreatmentId { get; set; }
        public Guid? RoomId { get; set; }
        public bool IsRecurring { get; set; }
        public int? RecurringIntervalDays { get; set; }
    }
}

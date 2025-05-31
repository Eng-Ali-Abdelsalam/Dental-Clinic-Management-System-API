using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public Guid DentistId { get; set; }
        public string DentistName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public AppointmentStatus Status { get; set; }
        public AppointmentType Type { get; set; }
        public Guid? TreatmentId { get; set; }
        public string TreatmentName { get; set; }
        public Guid? RoomId { get; set; }
        public string RoomName { get; set; }
        public bool IsRescheduled { get; set; }
        public bool IsRecurring { get; set; }
        public TimeSpan Duration { get; set; }
    }
}

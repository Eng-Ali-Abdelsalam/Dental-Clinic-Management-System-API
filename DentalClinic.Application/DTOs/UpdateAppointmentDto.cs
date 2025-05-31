using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class UpdateAppointmentDto
    {
        public Guid Id { get; set; }
        public Guid DentistId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public AppointmentStatus Status { get; set; }
        public AppointmentType Type { get; set; }
        public Guid? TreatmentId { get; set; }
        public Guid? RoomId { get; set; }
    }
}

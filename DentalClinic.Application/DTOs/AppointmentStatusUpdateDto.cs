using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class AppointmentStatusUpdateDto
    {
        public Guid Id { get; set; }
        public AppointmentStatus Status { get; set; }
        public string CancellationReason { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
    }
}

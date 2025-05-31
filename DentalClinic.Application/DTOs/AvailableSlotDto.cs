using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class AvailableSlotDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid DentistId { get; set; }
        public string DentistName { get; set; }
        public Guid? RoomId { get; set; }
        public string RoomName { get; set; }
    }
}

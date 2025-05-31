using System;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class AvailabilityRequestDto
    {
        public Guid? DentistId { get; set; }
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }
        public Guid? TreatmentId { get; set; }
    }
}

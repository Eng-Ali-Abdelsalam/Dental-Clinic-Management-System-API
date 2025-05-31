using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class DentistSchedule : BaseEntity
    {
        public Guid DentistId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsWorkDay { get; set; }

        // Navigation Property
        public Dentist Dentist { get; set; }
    }
}

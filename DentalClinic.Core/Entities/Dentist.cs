using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class Dentist : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string UserId { get; set; } // Reference to ASP.NET Identity User
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        // Navigation Properties
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();
        public ICollection<DentistSchedule> Schedules { get; set; } = new List<DentistSchedule>();

        public string FullName => $"Dr. {FirstName} {LastName}";
    }
}

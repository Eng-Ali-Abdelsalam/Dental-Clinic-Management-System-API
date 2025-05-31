using System;
using System.Collections.Generic;
using System.Reflection;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public string InsuranceGroupNumber { get; set; }
        public bool HasInsurance { get; set; }

        // Medical Information
        public string MedicalHistory { get; set; }
        public string CurrentMedications { get; set; }
        public string Allergies { get; set; }

        // Navigation Properties
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<TreatmentPlan> TreatmentPlans { get; set; } = new List<TreatmentPlan>();
        public ICollection<PatientDocument> Documents { get; set; } = new List<PatientDocument>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        public string FullName => $"{FirstName} {LastName}";
        public int Age => DateTime.Today.Year - DateOfBirth.Year - (DateTime.Today.DayOfYear < DateOfBirth.DayOfYear ? 1 : 0);
    }
}

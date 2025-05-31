using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class CreatePatientDto
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
        public string MedicalHistory { get; set; }
        public string CurrentMedications { get; set; }
        public string Allergies { get; set; }
    }
}

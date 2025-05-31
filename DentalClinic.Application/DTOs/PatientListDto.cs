using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class PatientListDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public DateTime LastAppointment { get; set; }
        public DateTime? NextAppointment { get; set; }
        public bool HasOutstandingBalance { get; set; }
    }
}

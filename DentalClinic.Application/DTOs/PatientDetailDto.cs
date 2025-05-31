using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class PatientDetailDto : PatientDto
    {
        public ICollection<AppointmentDto> Appointments { get; set; }
        public ICollection<TreatmentPlanDto> TreatmentPlans { get; set; }
        public ICollection<PatientDocumentDto> Documents { get; set; }
        public ICollection<InvoiceDto> Invoices { get; set; }
    }
}

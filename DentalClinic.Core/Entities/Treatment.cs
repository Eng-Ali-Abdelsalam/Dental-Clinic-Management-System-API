using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class Treatment : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal DefaultPrice { get; set; }
        public int EstimatedDurationMinutes { get; set; }
        public TreatmentCategory Category { get; set; }
        public bool RequiresFollowUp { get; set; }
        public int? RecommendedFollowUpDays { get; set; }

        // Navigation Properties
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<TreatmentPlanItem> TreatmentPlanItems { get; set; } = new List<TreatmentPlanItem>();
        public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
    }
}

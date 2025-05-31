using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class TreatmentPlan : BaseEntity
    {
        public Guid PatientId { get; set; }
        public Guid DentistId { get; set; }
        public string Title { get; set; }
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TreatmentPlanStatus Status { get; set; }
        public decimal TotalEstimatedCost { get; set; }

        // Navigation Properties
        public Patient Patient { get; set; }
        public Dentist Dentist { get; set; }
        public ICollection<TreatmentPlanItem> Items { get; set; } = new List<TreatmentPlanItem>();
    }
}

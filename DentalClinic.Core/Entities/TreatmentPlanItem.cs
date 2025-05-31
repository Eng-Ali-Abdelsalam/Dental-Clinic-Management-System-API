using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class TreatmentPlanItem
    {
        public Guid TreatmentPlanId { get; set; }
        public Guid TreatmentId { get; set; }
        public int Sequence { get; set; }
        public string Notes { get; set; }
        public TreatmentStatus Status { get; set; }
        public decimal Price { get; set; }
        public int? ToothNumber { get; set; }
        public DateTime? PlannedDate { get; set; }
        public DateTime? CompletedDate { get; set; }

        // Navigation Properties
        public TreatmentPlan TreatmentPlan { get; set; }
        public Treatment Treatment { get; set; }
    }
}

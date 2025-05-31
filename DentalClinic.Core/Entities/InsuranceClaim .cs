using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class InsuranceClaim : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public string ClaimNumber { get; set; }
        public DateTime SubmissionDate { get; set; }
        public ClaimStatus Status { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string RejectionReason { get; set; }
        public DateTime? ResponseDate { get; set; }

        // Navigation Property
        public Invoice Invoice { get; set; }
    }
}

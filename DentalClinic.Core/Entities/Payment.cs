using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class Payment : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public string ReferenceNumber { get; set; }
        public string Notes { get; set; }

        // Navigation Property
        public Invoice Invoice { get; set; }
    }
}

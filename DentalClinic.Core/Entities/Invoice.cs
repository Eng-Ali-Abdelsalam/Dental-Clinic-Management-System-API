using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Subtotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
        public InvoiceStatus Status { get; set; }

        // Navigation Properties
        public Patient Patient { get; set; }
        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();

        public decimal AmountPaid => Payments.Sum(p => p.Amount);
        public decimal BalanceDue => TotalAmount - AmountPaid;
        public bool IsPaid => BalanceDue <= 0;
    }
}

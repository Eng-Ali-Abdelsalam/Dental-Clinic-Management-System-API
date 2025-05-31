using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class InvoiceItem : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public Guid? TreatmentId { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal TaxRate { get; set; }
        public int? ToothNumber { get; set; }

        // Navigation Properties
        public Invoice Invoice { get; set; }
        public Treatment Treatment { get; set; }

        public decimal Subtotal => UnitPrice * Quantity;
        public decimal DiscountAmount => Subtotal * (Discount / 100);
        public decimal TaxAmount => (Subtotal - DiscountAmount) * (TaxRate / 100);
        public decimal TotalAmount => Subtotal - DiscountAmount + TaxAmount;
    }
}

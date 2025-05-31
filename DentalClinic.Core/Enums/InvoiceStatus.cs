using System;

namespace DentalClinic.Core.Enums
{
    public enum InvoiceStatus
    {
        Draft,
        Issued,
        PartiallyPaid,
        Paid,
        Overdue,
        Canceled,
        Refunded
    }
}

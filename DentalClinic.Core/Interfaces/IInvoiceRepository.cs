using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;

namespace DentalClinic.Core.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<Invoice> GetInvoiceWithDetailsAsync(Guid id);
        Task<IReadOnlyList<Invoice>> GetPatientInvoicesAsync(Guid patientId);
        Task<IReadOnlyList<Invoice>> GetOverdueInvoicesAsync();
    }
}

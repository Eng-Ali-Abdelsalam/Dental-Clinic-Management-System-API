using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Interfaces
{
    public interface ITreatmentRepository : IRepository<Treatment>
    {
        Task<IReadOnlyList<Treatment>> GetTreatmentsByCategoryAsync(TreatmentCategory category);
        Task<Treatment> GetTreatmentWithDetailsAsync(Guid id);
    }
}

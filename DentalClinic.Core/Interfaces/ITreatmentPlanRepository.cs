using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;

namespace DentalClinic.Core.Interfaces
{
    public interface ITreatmentPlanRepository : IRepository<TreatmentPlan>
    {
        Task<TreatmentPlan> GetTreatmentPlanWithItemsAsync(Guid id);
        Task<IReadOnlyList<TreatmentPlan>> GetPatientTreatmentPlansAsync(Guid patientId);
    }
}

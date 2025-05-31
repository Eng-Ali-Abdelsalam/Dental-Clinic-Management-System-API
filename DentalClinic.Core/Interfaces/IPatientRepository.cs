using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;

namespace DentalClinic.Core.Interfaces
{
    public interface IPatientRepository : IRepository<Patient>
    {
        Task<IReadOnlyList<Patient>> GetPatientsByNameAsync(string searchTerm);
        Task<Patient> GetPatientWithDetailsAsync(Guid id);
        Task<IReadOnlyList<Patient>> GetPatientsByFilterAsync(Expression<Func<Patient, bool>> filter, int skip, int take);
    }
}

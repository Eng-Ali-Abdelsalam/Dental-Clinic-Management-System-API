using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;

namespace DentalClinic.Core.Interfaces
{
    public interface IDentistRepository : IRepository<Dentist>
    {
        Task<Dentist> GetDentistWithScheduleAsync(Guid id);
        Task<IReadOnlyList<Dentist>> GetAvailableDentistsAsync(DateTime startTime, DateTime endTime);
    }
}

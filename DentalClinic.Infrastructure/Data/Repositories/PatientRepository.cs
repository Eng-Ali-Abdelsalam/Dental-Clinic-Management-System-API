using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Interfaces;
using DentalClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Data.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Patient>> GetPatientsByNameAsync(string searchTerm)
        {
            searchTerm = searchTerm?.ToLower() ?? string.Empty;

            return await _dbSet
                .Where(p => p.FirstName.ToLower().Contains(searchTerm) ||
                           p.LastName.ToLower().Contains(searchTerm))
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }

        public async Task<Patient> GetPatientWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Appointments)
                .ThenInclude(a => a.Dentist)
                .Include(p => p.Appointments)
                .ThenInclude(a => a.Treatment)
                .Include(p => p.TreatmentPlans)
                .ThenInclude(tp => tp.Items)
                .ThenInclude(tpi => tpi.Treatment)
                .Include(p => p.Documents)
                .Include(p => p.Invoices)
                .ThenInclude(i => i.Items)
                .Include(p => p.Invoices)
                .ThenInclude(i => i.Payments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Patient>> GetPatientsByFilterAsync(
            Expression<Func<Patient, bool>> filter, int skip, int take)
        {
            return await _dbSet
                .Where(filter)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}

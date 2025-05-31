using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Enums;
using DentalClinic.Core.Interfaces;
using DentalClinic.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Data.Repositories
{
    public class AppointmentRepository : BaseRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Appointment>> GetUpcomingAppointmentsAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Include(a => a.Treatment)
                .Include(a => a.Room)
                .Where(a => a.StartTime >= startDate && a.StartTime <= endDate &&
                           a.Status != AppointmentStatus.Canceled && a.Status != AppointmentStatus.NoShow)
                .OrderBy(a => a.StartTime)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Appointment>> GetDentistAppointmentsAsync(Guid dentistId, DateTime date)
        {
            var startDate = date.Date;
            var endDate = startDate.AddDays(1).AddTicks(-1);

            return await _dbSet
                .Include(a => a.Patient)
                .Include(a => a.Treatment)
                .Include(a => a.Room)
                .Where(a => a.DentistId == dentistId &&
                           a.StartTime >= startDate && a.StartTime <= endDate &&
                           a.Status != AppointmentStatus.Canceled)
                .OrderBy(a => a.StartTime)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Appointment>> GetPatientAppointmentsAsync(Guid patientId)
        {
            return await _dbSet
                .Include(a => a.Dentist)
                .Include(a => a.Treatment)
                .Include(a => a.Room)
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.StartTime)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Patient)
                .Include(a => a.Dentist)
                .Include(a => a.Treatment)
                .Include(a => a.Room)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}

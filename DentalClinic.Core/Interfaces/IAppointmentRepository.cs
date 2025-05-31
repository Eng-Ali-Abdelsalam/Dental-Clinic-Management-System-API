using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;

namespace DentalClinic.Core.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        Task<IReadOnlyList<Appointment>> GetUpcomingAppointmentsAsync(DateTime startDate, DateTime endDate);
        Task<IReadOnlyList<Appointment>> GetDentistAppointmentsAsync(Guid dentistId, DateTime date);
        Task<IReadOnlyList<Appointment>> GetPatientAppointmentsAsync(Guid patientId);
        Task<Appointment> GetAppointmentWithDetailsAsync(Guid id);
    }
}

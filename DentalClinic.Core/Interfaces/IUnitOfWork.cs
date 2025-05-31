using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;

namespace DentalClinic.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientRepository PatientRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }
        ITreatmentRepository TreatmentRepository { get; }
        ITreatmentPlanRepository TreatmentPlanRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }
        IDentistRepository DentistRepository { get; }
        IPatientDocumentRepository PatientDocumentRepository { get; }
        IRepository<Room> RoomRepository { get; }
        IRepository<Payment> PaymentRepository { get; }
        IRepository<InsuranceClaim> InsuranceClaimRepository { get; }

        Task<int> CompleteAsync();
    }
}

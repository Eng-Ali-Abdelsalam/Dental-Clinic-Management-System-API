using System;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Interfaces;
using DentalClinic.Infrastructure.Data.Repositories;

namespace DentalClinic.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IPatientRepository _patientRepository;
        private IAppointmentRepository _appointmentRepository;
        private ITreatmentRepository _treatmentRepository;
        private ITreatmentPlanRepository _treatmentPlanRepository;
        private IInvoiceRepository _invoiceRepository;
        private IDentistRepository _dentistRepository;
        private IPatientDocumentRepository _patientDocumentRepository;
        private IRepository<Room> _roomRepository;
        private IRepository<Payment> _paymentRepository;
        private IRepository<InsuranceClaim> _insuranceClaimRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IPatientRepository PatientRepository
            => _patientRepository ??= new PatientRepository(_context);

        public IAppointmentRepository AppointmentRepository
            => _appointmentRepository ??= new AppointmentRepository(_context);

        public ITreatmentRepository TreatmentRepository
            => _treatmentRepository ??= new TreatmentRepository(_context);

        public ITreatmentPlanRepository TreatmentPlanRepository
            => _treatmentPlanRepository ??= new TreatmentPlanRepository(_context);

        public IInvoiceRepository InvoiceRepository
            => _invoiceRepository ??= new InvoiceRepository(_context);

        public IDentistRepository DentistRepository
            => _dentistRepository ??= new DentistRepository(_context);

        public IPatientDocumentRepository PatientDocumentRepository
            => _patientDocumentRepository ??= new PatientDocumentRepository(_context);

        public IRepository<Room> RoomRepository
            => _roomRepository ??= new BaseRepository<Room>(_context);

        public IRepository<Payment> PaymentRepository
            => _paymentRepository ??= new BaseRepository<Payment>(_context);

        public IRepository<InsuranceClaim> InsuranceClaimRepository
            => _insuranceClaimRepository ??= new BaseRepository<InsuranceClaim>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

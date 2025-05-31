using System;
using System.Threading;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
        public DbSet<TreatmentPlanItem> TreatmentPlanItems { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<DentistSchedule> DentistSchedules { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }
        public DbSet<PatientDocument> PatientDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply entity configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Global query filter for soft delete
            modelBuilder.Entity<Patient>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Appointment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Treatment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<TreatmentPlan>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<TreatmentPlanItem>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Dentist>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Room>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Invoice>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<InvoiceItem>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Payment>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<InsuranceClaim>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<PatientDocument>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Automatically handle created/modified timestamps
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = GetCurrentUser();
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedAt = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = GetCurrentUser();
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        private string GetCurrentUser()
        {
            // In a real implementation, this would get the current user from the HttpContext
            // For now, we'll return a placeholder value
            return "system";
        }
    }
}

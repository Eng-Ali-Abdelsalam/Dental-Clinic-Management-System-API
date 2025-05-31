using System;
using System.Xml.Linq;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Entities
{
    public class PatientDocument : BaseEntity
    {
        public Guid PatientId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DocumentType Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public Guid? TreatmentId { get; set; }
        public Guid? AppointmentId { get; set; }

        // Navigation Properties
        public Patient Patient { get; set; }
        public Treatment Treatment { get; set; }
        public Appointment Appointment { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Enums;

namespace DentalClinic.Core.Interfaces
{
    public interface IPatientDocumentRepository : IRepository<PatientDocument>
    {
        Task<IReadOnlyList<PatientDocument>> GetPatientDocumentsAsync(Guid patientId);
        Task<IReadOnlyList<PatientDocument>> GetDocumentsByTypeAsync(Guid patientId, DocumentType type);
    }
}

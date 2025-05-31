using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalClinic.Application.DTOs;

namespace DentalClinic.Application.Interfaces
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientListDto>> GetAllPatientsAsync();
        Task<PatientDto> GetPatientByIdAsync(Guid id);
        Task<PatientDetailDto> GetPatientWithDetailsAsync(Guid id);
        Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto);
        Task UpdatePatientAsync(UpdatePatientDto updatePatientDto);
        Task DeletePatientAsync(Guid id);
        Task<IEnumerable<PatientListDto>> SearchPatientsByNameAsync(string searchTerm);
        Task<(IEnumerable<PatientListDto> Patients, int TotalCount)> GetPagedPatientsAsync(int pageNumber, int pageSize);
    }
}

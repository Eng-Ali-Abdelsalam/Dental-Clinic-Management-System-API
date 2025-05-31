using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalClinic.Application.DTOs;

namespace DentalClinic.Application.Interfaces
{
    public interface IAppointmentService
    {
        Task<AppointmentDto> GetAppointmentByIdAsync(Guid id);
        Task<AppointmentDetailDto> GetAppointmentWithDetailsAsync(Guid id);
        Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<AppointmentDto>> GetDentistAppointmentsAsync(Guid dentistId, DateTime date);
        Task<IEnumerable<AppointmentDto>> GetPatientAppointmentsAsync(Guid patientId);
        Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto);
        Task UpdateAppointmentAsync(UpdateAppointmentDto updateAppointmentDto);
        Task DeleteAppointmentAsync(Guid id);
        Task UpdateAppointmentStatusAsync(AppointmentStatusUpdateDto statusUpdateDto);
        Task<IEnumerable<AvailableSlotDto>> GetAvailableSlotsAsync(AvailabilityRequestDto requestDto);
    }
}

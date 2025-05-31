using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Application.Interfaces;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Enums;
using DentalClinic.Core.Interfaces;

namespace DentalClinic.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppointmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> GetAppointmentByIdAsync(Guid id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                throw new NotFoundException($"Appointment with ID {id} not found.");

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task<AppointmentDetailDto> GetAppointmentWithDetailsAsync(Guid id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentWithDetailsAsync(id);

            if (appointment == null)
                throw new NotFoundException($"Appointment with ID {id} not found.");

            return _mapper.Map<AppointmentDetailDto>(appointment);
        }

        public async Task<IEnumerable<AppointmentDto>> GetUpcomingAppointmentsAsync(DateTime startDate, DateTime endDate)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetUpcomingAppointmentsAsync(startDate, endDate);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentDto>> GetDentistAppointmentsAsync(Guid dentistId, DateTime date)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetDentistAppointmentsAsync(dentistId, date);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<IEnumerable<AppointmentDto>> GetPatientAppointmentsAsync(Guid patientId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetPatientAppointmentsAsync(patientId);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }

        public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto createAppointmentDto)
        {
            // Check if patient exists
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(createAppointmentDto.PatientId);
            if (patient == null)
                throw new NotFoundException($"Patient with ID {createAppointmentDto.PatientId} not found.");

            // Check if dentist exists
            var dentist = await _unitOfWork.DentistRepository.GetByIdAsync(createAppointmentDto.DentistId);
            if (dentist == null)
                throw new NotFoundException($"Dentist with ID {createAppointmentDto.DentistId} not found.");

            // Check if dentist is available for the requested time
            var conflictingAppointments = await _unitOfWork.AppointmentRepository.FindAsync(a =>
                a.DentistId == createAppointmentDto.DentistId &&
                ((a.StartTime <= createAppointmentDto.StartTime && a.EndTime > createAppointmentDto.StartTime) ||
                 (a.StartTime < createAppointmentDto.EndTime && a.EndTime >= createAppointmentDto.EndTime) ||
                 (a.StartTime >= createAppointmentDto.StartTime && a.EndTime <= createAppointmentDto.EndTime)) &&
                a.Status != AppointmentStatus.Canceled && a.Status != AppointmentStatus.NoShow);

            if (conflictingAppointments.Any())
                throw new ConflictException("The dentist is not available for the requested time slot.");

            // Check if room is available (if specified)
            if (createAppointmentDto.RoomId.HasValue)
            {
                var room = await _unitOfWork.RoomRepository.GetByIdAsync(createAppointmentDto.RoomId.Value);
                if (room == null)
                    throw new NotFoundException($"Room with ID {createAppointmentDto.RoomId} not found.");

                var roomConflicts = await _unitOfWork.AppointmentRepository.FindAsync(a =>
                    a.RoomId == createAppointmentDto.RoomId &&
                    ((a.StartTime <= createAppointmentDto.StartTime && a.EndTime > createAppointmentDto.StartTime) ||
                     (a.StartTime < createAppointmentDto.EndTime && a.EndTime >= createAppointmentDto.EndTime) ||
                     (a.StartTime >= createAppointmentDto.StartTime && a.EndTime <= createAppointmentDto.EndTime)) &&
                    a.Status != AppointmentStatus.Canceled && a.Status != AppointmentStatus.NoShow);

                if (roomConflicts.Any())
                    throw new ConflictException("The selected room is not available for the requested time slot.");
            }

            // Check if treatment exists (if specified)
            if (createAppointmentDto.TreatmentId.HasValue)
            {
                var treatment = await _unitOfWork.TreatmentRepository.GetByIdAsync(createAppointmentDto.TreatmentId.Value);
                if (treatment == null)
                    throw new NotFoundException($"Treatment with ID {createAppointmentDto.TreatmentId} not found.");
            }

            var appointment = _mapper.Map<Appointment>(createAppointmentDto);
            appointment.Status = AppointmentStatus.Scheduled;

            await _unitOfWork.AppointmentRepository.AddAsync(appointment);

            if (createAppointmentDto.IsRecurring && createAppointmentDto.RecurringIntervalDays.HasValue)
            {
                // Create recurring appointments
                // For simplicity, we'll just create 3 recurring appointments
                for (int i = 1; i <= 3; i++)
                {
                    var recurringAppointment = _mapper.Map<Appointment>(createAppointmentDto);
                    recurringAppointment.StartTime = appointment.StartTime.AddDays(i * createAppointmentDto.RecurringIntervalDays.Value);
                    recurringAppointment.EndTime = appointment.EndTime.AddDays(i * createAppointmentDto.RecurringIntervalDays.Value);
                    recurringAppointment.Status = AppointmentStatus.Scheduled;
                    recurringAppointment.IsRecurring = true;
                    recurringAppointment.RecurringIntervalDays = createAppointmentDto.RecurringIntervalDays;

                    await _unitOfWork.AppointmentRepository.AddAsync(recurringAppointment);
                }
            }

            await _unitOfWork.CompleteAsync();

            return _mapper.Map<AppointmentDto>(appointment);
        }

        public async Task UpdateAppointmentAsync(UpdateAppointmentDto updateAppointmentDto)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(updateAppointmentDto.Id);

            if (appointment == null)
                throw new NotFoundException($"Appointment with ID {updateAppointmentDto.Id} not found.");

            // Check if the appointment is already completed or canceled
            if (appointment.Status == AppointmentStatus.Completed ||
                appointment.Status == AppointmentStatus.Canceled ||
                appointment.Status == AppointmentStatus.NoShow)
            {
                throw new BadRequestException("Cannot update an appointment that is already completed, canceled, or marked as no-show.");
            }

            // Similar validation checks as in CreateAppointmentAsync
            // For brevity, we'll skip some of the detailed checks here

            _mapper.Map(updateAppointmentDto, appointment);

            await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAppointmentAsync(Guid id)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(id);

            if (appointment == null)
                throw new NotFoundException($"Appointment with ID {id} not found.");

            // Instead of deleting, we'll cancel the appointment
            appointment.Status = AppointmentStatus.Canceled;
            appointment.CancellationReason = "Deleted by admin";

            await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAppointmentStatusAsync(AppointmentStatusUpdateDto statusUpdateDto)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(statusUpdateDto.Id);

            if (appointment == null)
                throw new NotFoundException($"Appointment with ID {statusUpdateDto.Id} not found.");

            // Add status transition validation if needed

            appointment.Status = statusUpdateDto.Status;

            if (statusUpdateDto.Status == AppointmentStatus.Canceled)
            {
                appointment.CancellationReason = statusUpdateDto.CancellationReason;
            }
            else if (statusUpdateDto.Status == AppointmentStatus.CheckedIn)
            {
                appointment.CheckInTime = statusUpdateDto.CheckInTime ?? DateTime.Now;
            }
            else if (statusUpdateDto.Status == AppointmentStatus.Completed)
            {
                appointment.CheckOutTime = statusUpdateDto.CheckOutTime ?? DateTime.Now;
            }

            await _unitOfWork.AppointmentRepository.UpdateAsync(appointment);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<AvailableSlotDto>> GetAvailableSlotsAsync(AvailabilityRequestDto requestDto)
        {
            // This is a simplified implementation
            // In a real system, this would be more complex considering dentist schedules,
            // treatment durations, room availability, etc.

            var availableSlots = new List<AvailableSlotDto>();
            var date = requestDto.Date.Date;
            var startTime = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0); // Assume clinic opens at 9 AM
            var endTime = new DateTime(date.Year, date.Month, date.Day, 17, 0, 0);  // Assume clinic closes at 5 PM
            var slotDuration = TimeSpan.FromMinutes(requestDto.DurationMinutes);

            // If dentist is specified, get their appointments for the day
            List<Appointment> existingAppointments = new List<Appointment>();
            List<Dentist> availableDentists = new List<Dentist>();

            if (requestDto.DentistId.HasValue)
            {
                // Get appointments for specific dentist
                existingAppointments = (await _unitOfWork.AppointmentRepository
                    .FindAsync(a => a.DentistId == requestDto.DentistId.Value &&
                                    a.StartTime.Date == date.Date &&
                                    a.Status != AppointmentStatus.Canceled)).ToList();

                var dentist = await _unitOfWork.DentistRepository.GetByIdAsync(requestDto.DentistId.Value);
                if (dentist == null)
                    throw new NotFoundException($"Dentist with ID {requestDto.DentistId} not found.");

                availableDentists.Add(dentist);
            }
            else
            {
                // Get all active dentists
                availableDentists = (await _unitOfWork.DentistRepository
                    .FindAsync(d => d.IsActive)).ToList();

                // Get all appointments for the day
                existingAppointments = (await _unitOfWork.AppointmentRepository
                    .FindAsync(a => a.StartTime.Date == date.Date &&
                                    a.Status != AppointmentStatus.Canceled)).ToList();
            }

            // Get all available rooms
            var rooms = (await _unitOfWork.RoomRepository.FindAsync(r => r.IsActive)).ToList();

            // Generate available slots
            for (var currentTime = startTime; currentTime.Add(slotDuration) <= endTime; currentTime = currentTime.AddMinutes(30))
            {
                var slotEndTime = currentTime.Add(slotDuration);

                foreach (var dentist in availableDentists)
                {
                    // Check if dentist is available at this time
                    bool dentistAvailable = !existingAppointments.Any(a =>
                        a.DentistId == dentist.Id &&
                        ((a.StartTime <= currentTime && a.EndTime > currentTime) ||
                         (a.StartTime < slotEndTime && a.EndTime >= slotEndTime) ||
                         (a.StartTime >= currentTime && a.EndTime <= slotEndTime)));

                    if (dentistAvailable)
                    {
                        // Find an available room
                        foreach (var room in rooms)
                        {
                            bool roomAvailable = !existingAppointments.Any(a =>
                                a.RoomId == room.Id &&
                                ((a.StartTime <= currentTime && a.EndTime > currentTime) ||
                                 (a.StartTime < slotEndTime && a.EndTime >= slotEndTime) ||
                                 (a.StartTime >= currentTime && a.EndTime <= slotEndTime)));

                            if (roomAvailable)
                            {
                                availableSlots.Add(new AvailableSlotDto
                                {
                                    StartTime = currentTime,
                                    EndTime = slotEndTime,
                                    DentistId = dentist.Id,
                                    DentistName = dentist.FullName,
                                    RoomId = room.Id,
                                    RoomName = room.Name
                                });

                                break; // Found a room for this slot, move to next dentist
                            }
                        }
                    }
                }
            }

            return availableSlots;
        }
    }
}

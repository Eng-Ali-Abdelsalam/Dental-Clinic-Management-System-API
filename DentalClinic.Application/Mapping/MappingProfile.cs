using AutoMapper;
using DentalClinic.Application.DTOs;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Patient mappings
            CreateMap<Patient, PatientDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    DateTime.Today.Year - src.DateOfBirth.Year - (DateTime.Today.DayOfYear < src.DateOfBirth.DayOfYear ? 1 : 0)));

            CreateMap<Patient, PatientDetailDto>()
                .IncludeBase<Patient, PatientDto>();

            CreateMap<CreatePatientDto, Patient>();
            CreateMap<UpdatePatientDto, Patient>();

            CreateMap<Patient, PatientListDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    DateTime.Today.Year - src.DateOfBirth.Year - (DateTime.Today.DayOfYear < src.DateOfBirth.DayOfYear ? 1 : 0)))
                .ForMember(dest => dest.LastAppointment, opt => opt.MapFrom(src =>
                    src.Appointments.Where(a => a.Status == AppointmentStatus.Completed)
                        .OrderByDescending(a => a.EndTime)
                        .Select(a => a.EndTime)
                        .FirstOrDefault()))
                .ForMember(dest => dest.NextAppointment, opt => opt.MapFrom(src =>
                    src.Appointments.Where(a => a.StartTime > DateTime.Now && a.Status != AppointmentStatus.Canceled)
                        .OrderBy(a => a.StartTime)
                        .Select(a => a.StartTime)
                        .FirstOrDefault()))
                .ForMember(dest => dest.HasOutstandingBalance, opt => opt.MapFrom(src =>
                    src.Invoices.Any(i => i.Status != InvoiceStatus.Paid && i.Status != InvoiceStatus.Canceled)));

            // Appointment mappings
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.PatientName, opt => opt.MapFrom(src => src.Patient.FullName))
                .ForMember(dest => dest.DentistName, opt => opt.MapFrom(src => src.Dentist.FullName))
                .ForMember(dest => dest.TreatmentName, opt => opt.MapFrom(src => src.Treatment != null ? src.Treatment.Name : null))
                .ForMember(dest => dest.RoomName, opt => opt.MapFrom(src => src.Room != null ? src.Room.Name : null))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.EndTime - src.StartTime));

            CreateMap<Appointment, AppointmentDetailDto>()
                .IncludeBase<Appointment, AppointmentDto>();

            CreateMap<CreateAppointmentDto, Appointment>();
            CreateMap<UpdateAppointmentDto, Appointment>();

            // Other mappings can be added as needed
        }
    }
}

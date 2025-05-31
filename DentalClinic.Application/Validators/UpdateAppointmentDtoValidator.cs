using System;
using DentalClinic.Application.DTOs;
using FluentValidation;

namespace DentalClinic.Application.Validators
{
    public class UpdateAppointmentDtoValidator : AbstractValidator<UpdateAppointmentDto>
    {
        public UpdateAppointmentDtoValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Appointment ID is required");

            RuleFor(a => a.DentistId)
                .NotEmpty().WithMessage("Dentist ID is required");

            RuleFor(a => a.StartTime)
                .NotEmpty().WithMessage("Start time is required");

            RuleFor(a => a.EndTime)
                .NotEmpty().WithMessage("End time is required")
                .GreaterThan(a => a.StartTime).WithMessage("End time must be after start time");

            RuleFor(a => a.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(a => a.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");
        }
    }
}

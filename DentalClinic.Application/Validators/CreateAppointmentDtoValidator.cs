using System;
using DentalClinic.Application.DTOs;
using FluentValidation;

namespace DentalClinic.Application.Validators
{
    public class CreateAppointmentDtoValidator : AbstractValidator<CreateAppointmentDto>
    {
        public CreateAppointmentDtoValidator()
        {
            RuleFor(a => a.PatientId)
                .NotEmpty().WithMessage("Patient ID is required");

            RuleFor(a => a.DentistId)
                .NotEmpty().WithMessage("Dentist ID is required");

            RuleFor(a => a.StartTime)
                .NotEmpty().WithMessage("Start time is required")
                .GreaterThan(DateTime.Now).WithMessage("Start time must be in the future");

            RuleFor(a => a.EndTime)
                .NotEmpty().WithMessage("End time is required")
                .GreaterThan(a => a.StartTime).WithMessage("End time must be after start time");

            RuleFor(a => a.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(a => a.Notes)
                .MaximumLength(1000).WithMessage("Notes cannot exceed 1000 characters");

            RuleFor(a => a.RecurringIntervalDays)
                .GreaterThan(0).When(a => a.IsRecurring)
                .WithMessage("Recurring interval must be greater than 0 when appointment is recurring");
        }
    }
}

using System;
using DentalClinic.Application.DTOs;
using FluentValidation;

namespace DentalClinic.Application.Validators
{
    public class AppointmentStatusUpdateDtoValidator : AbstractValidator<AppointmentStatusUpdateDto>
    {
        public AppointmentStatusUpdateDtoValidator()
        {
            RuleFor(a => a.Id)
                .NotEmpty().WithMessage("Appointment ID is required");

            RuleFor(a => a.Status)
                .IsInEnum().WithMessage("Invalid appointment status");

            RuleFor(a => a.CancellationReason)
                .NotEmpty().When(a => a.Status == Core.Enums.AppointmentStatus.Canceled)
                .WithMessage("Cancellation reason is required when canceling an appointment")
                .MaximumLength(500).WithMessage("Cancellation reason cannot exceed 500 characters");
        }
    }
}

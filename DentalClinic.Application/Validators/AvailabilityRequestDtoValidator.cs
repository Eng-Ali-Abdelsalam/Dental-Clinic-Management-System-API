using System;
using DentalClinic.Application.DTOs;
using FluentValidation;

namespace DentalClinic.Application.Validators
{
    public class AvailabilityRequestDtoValidator : AbstractValidator<AvailabilityRequestDto>
    {
        public AvailabilityRequestDtoValidator()
        {
            RuleFor(a => a.Date)
                .NotEmpty().WithMessage("Date is required")
                .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Date must be today or in the future");

            RuleFor(a => a.DurationMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes")
                .LessThanOrEqualTo(480).WithMessage("Duration cannot exceed 8 hours (480 minutes)");
        }
    }
}

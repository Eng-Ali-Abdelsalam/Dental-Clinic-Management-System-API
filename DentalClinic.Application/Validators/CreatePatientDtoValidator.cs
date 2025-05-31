using System;
using DentalClinic.Application.DTOs;
using FluentValidation;

namespace DentalClinic.Application.Validators
{
    public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientDtoValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters");

            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                .LessThan(DateTime.Now).WithMessage("Date of birth cannot be in the future");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9\s\-\(\)]+$").WithMessage("Invalid phone number format")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters");

            RuleFor(p => p.Address)
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters");

            RuleFor(p => p.City)
                .MaximumLength(100).WithMessage("City cannot exceed 100 characters");

            RuleFor(p => p.State)
                .MaximumLength(50).WithMessage("State cannot exceed 50 characters");

            RuleFor(p => p.ZipCode)
                .MaximumLength(20).WithMessage("Zip code cannot exceed 20 characters");

            RuleFor(p => p.EmergencyContactName)
                .MaximumLength(100).WithMessage("Emergency contact name cannot exceed 100 characters");

            RuleFor(p => p.EmergencyContactPhone)
                .Matches(@"^\+?[0-9\s\-\(\)]+$").When(p => !string.IsNullOrEmpty(p.EmergencyContactPhone))
                .WithMessage("Invalid emergency contact phone number format")
                .MaximumLength(20).WithMessage("Emergency contact phone cannot exceed 20 characters");

            RuleFor(p => p.InsurancePolicyNumber)
                .MaximumLength(50).WithMessage("Insurance policy number cannot exceed 50 characters");

            RuleFor(p => p.InsuranceGroupNumber)
                .MaximumLength(50).WithMessage("Insurance group number cannot exceed 50 characters");

            RuleFor(p => p.MedicalHistory)
                .MaximumLength(2000).WithMessage("Medical history cannot exceed 2000 characters");

            RuleFor(p => p.CurrentMedications)
                .MaximumLength(1000).WithMessage("Current medications cannot exceed 1000 characters");

            RuleFor(p => p.Allergies)
                .MaximumLength(1000).WithMessage("Allergies cannot exceed 1000 characters");
        }
    }
}

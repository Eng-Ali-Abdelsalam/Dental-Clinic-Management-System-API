using System;
using DentalClinic.Application.DTOs;
using FluentValidation;

namespace DentalClinic.Application.Validators
{
    public class UpdatePatientDtoValidator : AbstractValidator<UpdatePatientDto>
    {
        public UpdatePatientDtoValidator()
        {
            RuleFor(p => p.Id)
                .NotEmpty().WithMessage("ID is required");

            // Reuse validation rules from CreatePatientDtoValidator
            Include(new CreatePatientDtoValidator());
        }
    }
}

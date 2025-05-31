using System;
using System.Threading;
using System.Threading.Tasks;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace DentalClinic.Application.Commands
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, AppointmentDto>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IValidator<CreateAppointmentDto> _validator;

        public CreateAppointmentCommandHandler(
            IAppointmentService appointmentService,
            IValidator<CreateAppointmentDto> validator)
        {
            _appointmentService = appointmentService;
            _validator = validator;
        }

        public async Task<AppointmentDto> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.AppointmentDto, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            return await _appointmentService.CreateAppointmentAsync(request.AppointmentDto);
        }
    }
}

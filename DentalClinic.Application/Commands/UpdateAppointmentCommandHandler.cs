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
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IValidator<UpdateAppointmentDto> _validator;

        public UpdateAppointmentCommandHandler(
            IAppointmentService appointmentService,
            IValidator<UpdateAppointmentDto> validator)
        {
            _appointmentService = appointmentService;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.AppointmentDto, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            await _appointmentService.UpdateAppointmentAsync(request.AppointmentDto);
            return Unit.Value;
        }
    }
}

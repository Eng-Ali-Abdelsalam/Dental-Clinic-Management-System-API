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
    public class UpdateAppointmentStatusCommandHandler : IRequestHandler<UpdateAppointmentStatusCommand>
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IValidator<AppointmentStatusUpdateDto> _validator;

        public UpdateAppointmentStatusCommandHandler(
            IAppointmentService appointmentService,
            IValidator<AppointmentStatusUpdateDto> validator)
        {
            _appointmentService = appointmentService;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.StatusUpdateDto, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            await _appointmentService.UpdateAppointmentStatusAsync(request.StatusUpdateDto);
            return Unit.Value;
        }
    }
}

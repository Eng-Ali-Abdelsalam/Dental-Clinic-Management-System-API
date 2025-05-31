using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Application.Interfaces;
using DentalClinic.Core.Interfaces;
using MediatR;

namespace DentalClinic.Application.Queries
{
    public class GetAvailableSlotsQueryHandler : IRequestHandler<GetAvailableSlotsQuery, IEnumerable<AvailableSlotDto>>
    {
        private readonly IAppointmentService _appointmentService;

        public GetAvailableSlotsQueryHandler(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public async Task<IEnumerable<AvailableSlotDto>> Handle(GetAvailableSlotsQuery request, CancellationToken cancellationToken)
        {
            return await _appointmentService.GetAvailableSlotsAsync(request.RequestDto);
        }
    }
}

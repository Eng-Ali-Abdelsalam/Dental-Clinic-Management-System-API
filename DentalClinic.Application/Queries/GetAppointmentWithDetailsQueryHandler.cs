using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Core.Interfaces;
using MediatR;

namespace DentalClinic.Application.Queries
{
    public class GetAppointmentWithDetailsQueryHandler : IRequestHandler<GetAppointmentWithDetailsQuery, AppointmentDetailDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAppointmentWithDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppointmentDetailDto> Handle(GetAppointmentWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetAppointmentWithDetailsAsync(request.Id);

            if (appointment == null)
                throw new NotFoundException($"Appointment with ID {request.Id} not found.");

            return _mapper.Map<AppointmentDetailDto>(appointment);
        }
    }
}

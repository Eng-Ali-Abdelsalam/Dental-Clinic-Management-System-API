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
    public class GetDentistAppointmentsQueryHandler : IRequestHandler<GetDentistAppointmentsQuery, IEnumerable<AppointmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDentistAppointmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AppointmentDto>> Handle(GetDentistAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetDentistAppointmentsAsync(request.DentistId, request.Date);
            return _mapper.Map<IEnumerable<AppointmentDto>>(appointments);
        }
    }
}

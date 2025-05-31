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
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.Id);

            if (patient == null)
                throw new NotFoundException($"Patient with ID {request.Id} not found.");

            return _mapper.Map<PatientDto>(patient);
        }
    }
}

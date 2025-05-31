using AutoMapper;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Application.Queries
{
    public class GetPatientWithDetailsQueryHandler : IRequestHandler<GetPatientWithDetailsQuery, PatientDetailDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientWithDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PatientDetailDto> Handle(GetPatientWithDetailsQuery request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientWithDetailsAsync(request.Id);

            if (patient == null)
                throw new NotFoundException($"Patient with ID {request.Id} not found.");

            return _mapper.Map<PatientDetailDto>(patient);
        }
    }
}

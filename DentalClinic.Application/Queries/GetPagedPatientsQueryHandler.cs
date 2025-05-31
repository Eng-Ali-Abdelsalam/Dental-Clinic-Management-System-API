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
    public class GetPagedPatientsQueryHandler : IRequestHandler<GetPagedPatientsQuery, (IEnumerable<PatientListDto> Patients, int TotalCount)>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPagedPatientsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<PatientListDto> Patients, int TotalCount)> Handle(
            GetPagedPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _unitOfWork.PatientRepository.GetPatientsByFilterAsync(
                p => true, (request.PageNumber - 1) * request.PageSize, request.PageSize);

            var totalCount = await _unitOfWork.PatientRepository.CountAsync(p => true);

            return (_mapper.Map<IEnumerable<PatientListDto>>(patients), totalCount);
        }
    }
}

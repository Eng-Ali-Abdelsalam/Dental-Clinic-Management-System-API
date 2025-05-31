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
    public class SearchPatientsByNameQueryHandler : IRequestHandler<SearchPatientsByNameQuery, IEnumerable<PatientListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchPatientsByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientListDto>> Handle(SearchPatientsByNameQuery request, CancellationToken cancellationToken)
        {
            var patients = await _unitOfWork.PatientRepository.GetPatientsByNameAsync(request.SearchTerm);
            return _mapper.Map<IEnumerable<PatientListDto>>(patients);
        }
    }
}

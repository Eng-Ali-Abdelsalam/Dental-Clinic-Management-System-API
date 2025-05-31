using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Interfaces;
using FluentValidation;
using MediatR;

namespace DentalClinic.Application.Commands
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePatientDto> _validator;

        public CreatePatientCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<CreatePatientDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<PatientDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.PatientDto, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            var patient = _mapper.Map<Patient>(request.PatientDto);

            await _unitOfWork.PatientRepository.AddAsync(patient);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PatientDto>(patient);
        }
    }
}

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
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdatePatientDto> _validator;

        public UpdatePatientCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<UpdatePatientDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Unit> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.PatientDto, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(string.Join(", ", validationResult.Errors));

            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(request.PatientDto.Id);

            if (patient == null)
                throw new NotFoundException($"Patient with ID {request.PatientDto.Id} not found.");

            _mapper.Map(request.PatientDto, patient);

            await _unitOfWork.PatientRepository.UpdateAsync(patient);
            await _unitOfWork.CompleteAsync();

            return Unit.Value;
        }
    }
}

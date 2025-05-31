using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Application.Interfaces;
using DentalClinic.Core.Entities;
using DentalClinic.Core.Interfaces;

namespace DentalClinic.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PatientListDto>> GetAllPatientsAsync()
        {
            var patients = await _unitOfWork.PatientRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PatientListDto>>(patients);
        }

        public async Task<PatientDto> GetPatientByIdAsync(Guid id)
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(id);

            if (patient == null)
                throw new NotFoundException($"Patient with ID {id} not found.");

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDetailDto> GetPatientWithDetailsAsync(Guid id)
        {
            var patient = await _unitOfWork.PatientRepository.GetPatientWithDetailsAsync(id);

            if (patient == null)
                throw new NotFoundException($"Patient with ID {id} not found.");

            return _mapper.Map<PatientDetailDto>(patient);
        }

        public async Task<PatientDto> CreatePatientAsync(CreatePatientDto createPatientDto)
        {
            var patient = _mapper.Map<Patient>(createPatientDto);

            await _unitOfWork.PatientRepository.AddAsync(patient);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task UpdatePatientAsync(UpdatePatientDto updatePatientDto)
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(updatePatientDto.Id);

            if (patient == null)
                throw new NotFoundException($"Patient with ID {updatePatientDto.Id} not found.");

            _mapper.Map(updatePatientDto, patient);

            await _unitOfWork.PatientRepository.UpdateAsync(patient);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeletePatientAsync(Guid id)
        {
            var patient = await _unitOfWork.PatientRepository.GetByIdAsync(id);

            if (patient == null)
                throw new NotFoundException($"Patient with ID {id} not found.");

            await _unitOfWork.PatientRepository.DeleteAsync(patient);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<PatientListDto>> SearchPatientsByNameAsync(string searchTerm)
        {
            var patients = await _unitOfWork.PatientRepository.GetPatientsByNameAsync(searchTerm);
            return _mapper.Map<IEnumerable<PatientListDto>>(patients);
        }

        public async Task<(IEnumerable<PatientListDto> Patients, int TotalCount)> GetPagedPatientsAsync(int pageNumber, int pageSize)
        {
            var patients = await _unitOfWork.PatientRepository.GetPatientsByFilterAsync(
                p => true, (pageNumber - 1) * pageSize, pageSize);

            var totalCount = await _unitOfWork.PatientRepository.CountAsync(p => true);

            return (_mapper.Map<IEnumerable<PatientListDto>>(patients), totalCount);
        }
    }
}

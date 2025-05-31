using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalClinic.Application.Commands;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Interfaces;
using DentalClinic.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a list of all patients
        /// </summary>
        /// <returns>A list of patients</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PatientListDto>), 200)]
        public async Task<ActionResult<IEnumerable<PatientListDto>>> GetAllPatients()
        {
            var patients = await _mediator.Send(new GetAllPatientsQuery());
            return Ok(patients);
        }

        /// <summary>
        /// Gets a specific patient by id
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>The patient details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PatientDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PatientDto>> GetPatient(Guid id)
        {
            var patient = await _mediator.Send(new GetPatientByIdQuery { Id = id });
            return Ok(patient);
        }

        /// <summary>
        /// Gets detailed information for a patient
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>Detailed patient information</returns>
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(PatientDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PatientDetailDto>> GetPatientDetails(Guid id)
        {
            var patientDetails = await _mediator.Send(new GetPatientWithDetailsQuery { Id = id });
            return Ok(patientDetails);
        }

        /// <summary>
        /// Creates a new patient
        /// </summary>
        /// <param name="createPatientDto">The patient data</param>
        /// <returns>The newly created patient</returns>
        [HttpPost]
        [ProducesResponseType(typeof(PatientDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PatientDto>> CreatePatient(CreatePatientDto createPatientDto)
        {
            var patient = await _mediator.Send(new CreatePatientCommand { PatientDto = createPatientDto });
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Updates an existing patient
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <param name="updatePatientDto">The updated patient data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdatePatient(Guid id, UpdatePatientDto updatePatientDto)
        {
            if (id != updatePatientDto.Id)
                return BadRequest("ID in the URL does not match the ID in the request body");

            await _mediator.Send(new UpdatePatientCommand { PatientDto = updatePatientDto });
            return NoContent();
        }

        /// <summary>
        /// Deletes a patient
        /// </summary>
        /// <param name="id">The patient id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            await _mediator.Send(new DeletePatientCommand { Id = id });
            return NoContent();
        }

        /// <summary>
        /// Searches for patients by name
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <returns>A list of matching patients</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IEnumerable<PatientListDto>), 200)]
        public async Task<ActionResult<IEnumerable<PatientListDto>>> SearchPatients([FromQuery] string searchTerm)
        {
            var patients = await _mediator.Send(new SearchPatientsByNameQuery { SearchTerm = searchTerm });
            return Ok(patients);
        }

        /// <summary>
        /// Gets a paged list of patients
        /// </summary>
        /// <param name="pageNumber">The page number</param>
        /// <param name="pageSize">The page size</param>
        /// <returns>A paged list of patients</returns>
        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResponse<PatientListDto>), 200)]
        public async Task<ActionResult<PagedResponse<PatientListDto>>> GetPagedPatients(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var (patients, totalCount) = await _mediator.Send(new GetPagedPatientsQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            var response = new PagedResponse<PatientListDto>
            {
                Data = patients,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Ok(response);
        }
    }

    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }
}

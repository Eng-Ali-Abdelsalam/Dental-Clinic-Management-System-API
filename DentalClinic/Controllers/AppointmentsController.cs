using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalClinic.Application.Commands;
using DentalClinic.Application.DTOs;
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
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets an appointment by id
        /// </summary>
        /// <param name="id">The appointment id</param>
        /// <returns>The appointment</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppointmentDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AppointmentDto>> GetAppointment(Guid id)
        {
            var appointment = await _mediator.Send(new GetAppointmentByIdQuery { Id = id });
            return Ok(appointment);
        }

        /// <summary>
        /// Gets detailed appointment information by id
        /// </summary>
        /// <param name="id">The appointment id</param>
        /// <returns>The detailed appointment</returns>
        [HttpGet("{id}/details")]
        [ProducesResponseType(typeof(AppointmentDetailDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<AppointmentDetailDto>> GetAppointmentDetails(Guid id)
        {
            var appointmentDetails = await _mediator.Send(new GetAppointmentWithDetailsQuery { Id = id });
            return Ok(appointmentDetails);
        }

        /// <summary>
        /// Gets upcoming appointments within a date range
        /// </summary>
        /// <param name="startDate">The start date</param>
        /// <param name="endDate">The end date</param>
        /// <returns>A list of appointments</returns>
        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), 200)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetUpcomingAppointments(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var appointments = await _mediator.Send(new GetUpcomingAppointmentsQuery
            {
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(appointments);
        }

        /// <summary>
        /// Gets appointments for a dentist on a specific date
        /// </summary>
        /// <param name="dentistId">The dentist id</param>
        /// <param name="date">The date</param>
        /// <returns>A list of appointments</returns>
        [HttpGet("dentist/{dentistId}")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), 200)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetDentistAppointments(
            Guid dentistId,
            [FromQuery] DateTime date)
        {
            var appointments = await _mediator.Send(new GetDentistAppointmentsQuery
            {
                DentistId = dentistId,
                Date = date
            });
            return Ok(appointments);
        }

        /// <summary>
        /// Gets appointments for a patient
        /// </summary>
        /// <param name="patientId">The patient id</param>
        /// <returns>A list of appointments</returns>
        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentDto>), 200)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetPatientAppointments(Guid patientId)
        {
            var appointments = await _mediator.Send(new GetPatientAppointmentsQuery { PatientId = patientId });
            return Ok(appointments);
        }

        /// <summary>
        /// Creates a new appointment
        /// </summary>
        /// <param name="createAppointmentDto">The appointment data</param>
        /// <returns>The newly created appointment</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AppointmentDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AppointmentDto>> CreateAppointment(CreateAppointmentDto createAppointmentDto)
        {
            var appointment = await _mediator.Send(new CreateAppointmentCommand
            {
                AppointmentDto = createAppointmentDto
            });
            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.Id }, appointment);
        }

        /// <summary>
        /// Updates an existing appointment
        /// </summary>
        /// <param name="id">The appointment id</param>
        /// <param name="updateAppointmentDto">The updated appointment data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAppointment(Guid id, UpdateAppointmentDto updateAppointmentDto)
        {
            if (id != updateAppointmentDto.Id)
                return BadRequest("ID in the URL does not match the ID in the request body");

            await _mediator.Send(new UpdateAppointmentCommand { AppointmentDto = updateAppointmentDto });
            return NoContent();
        }

        /// <summary>
        /// Deletes an appointment
        /// </summary>
        /// <param name="id">The appointment id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            await _mediator.Send(new DeleteAppointmentCommand { Id = id });
            return NoContent();
        }

        /// <summary>
        /// Updates the status of an appointment
        /// </summary>
        /// <param name="id">The appointment id</param>
        /// <param name="statusUpdateDto">The status update data</param>
        /// <returns>No content</returns>
        [HttpPatch("{id}/status")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateAppointmentStatus(Guid id, AppointmentStatusUpdateDto statusUpdateDto)
        {
            if (id != statusUpdateDto.Id)
                return BadRequest("ID in the URL does not match the ID in the request body");

            await _mediator.Send(new UpdateAppointmentStatusCommand { StatusUpdateDto = statusUpdateDto });
            return NoContent();
        }

        /// <summary>
        /// Gets available appointment slots
        /// </summary>
        /// <param name="requestDto">The availability request data</param>
        /// <returns>A list of available slots</returns>
        [HttpPost("available-slots")]
        [ProducesResponseType(typeof(IEnumerable<AvailableSlotDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<AvailableSlotDto>>> GetAvailableSlots(AvailabilityRequestDto requestDto)
        {
            var slots = await _mediator.Send(new GetAvailableSlotsQuery { RequestDto = requestDto });
            return Ok(slots);
        }
    }
}

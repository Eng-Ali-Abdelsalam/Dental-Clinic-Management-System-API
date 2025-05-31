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
    public class GetDentistAppointmentsQuery : IRequest<IEnumerable<AppointmentDto>>
    {
        public Guid DentistId { get; set; }
        public DateTime Date { get; set; }
    }
}

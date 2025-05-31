using System;
using System.Threading;
using System.Threading.Tasks;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Exceptions;
using DentalClinic.Application.Interfaces;
using FluentValidation;
using MediatR;

namespace DentalClinic.Application.Commands
{
    public class CreateAppointmentCommand : IRequest<AppointmentDto>
    {
        public CreateAppointmentDto AppointmentDto { get; set; }
    }
}

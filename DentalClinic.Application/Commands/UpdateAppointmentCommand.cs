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
    public class UpdateAppointmentCommand : IRequest
    {
        public UpdateAppointmentDto AppointmentDto { get; set; }
    }
}

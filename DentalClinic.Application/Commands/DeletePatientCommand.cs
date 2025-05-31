using System;
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
    public class DeletePatientCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

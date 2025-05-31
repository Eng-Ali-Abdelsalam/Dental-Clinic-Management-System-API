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
    public class SearchPatientsByNameQuery : IRequest<IEnumerable<PatientListDto>>
    {
        public string SearchTerm { get; set; }
    }
}

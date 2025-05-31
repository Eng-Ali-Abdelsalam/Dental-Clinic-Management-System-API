using System;
using System.Collections.Generic;
using DentalClinic.Core.Enums;

namespace DentalClinic.Application.DTOs
{
    public class UpdatePatientDto : CreatePatientDto
    {
        public Guid Id { get; set; }
    }
}

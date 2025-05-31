using System;
using System.Collections.Generic;

namespace DentalClinic.Application.DTOs
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

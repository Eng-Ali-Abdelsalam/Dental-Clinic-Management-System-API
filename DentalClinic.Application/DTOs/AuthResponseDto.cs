﻿using System;
using System.Collections.Generic;

namespace DentalClinic.Application.DTOs
{
    public class AuthResponseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}

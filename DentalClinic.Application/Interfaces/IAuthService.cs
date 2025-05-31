using System;
using System.Threading.Tasks;
using DentalClinic.Application.DTOs;

namespace DentalClinic.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginRequestDto request);
        Task<RegistrationResponseDto> RegisterAsync(RegistrationRequestDto request);
        Task<string> GenerateJwtToken(IdentityUser user);
    }
}

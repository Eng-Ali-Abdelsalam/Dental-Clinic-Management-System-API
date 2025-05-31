using System.Threading.Tasks;
using DentalClinic.Application.DTOs;
using DentalClinic.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinic.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticates a user
        /// </summary>
        /// <param name="request">The login request</param>
        /// <returns>Authentication response with JWT token</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginRequestDto request)
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="request">The registration request</param>
        /// <returns>Registration response</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(RegistrationResponseDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<RegistrationResponseDto>> Register(RegistrationRequestDto request)
        {
            var response = await _authService.RegisterAsync(request);
            return Created(string.Empty, response);
        }
    }
}

using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/auths")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => this._authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto authDto)
        {
            if (authDto == null || string.IsNullOrEmpty(authDto.Email) || string.IsNullOrEmpty(authDto.Password))
            {
                return BadRequest(new { message = "Email и пароль обязательны." });
            }

            try
            {
                var token = await _authService.Login(authDto);
                return Ok(new { token });
            }
            catch (ServiceException ex)
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCode.NotFound:
                        return NotFound(new { message = ex.Message });
                    case ErrorCode.Unauthorized:
                        return Unauthorized(new { message = ex.Message });
                    case ErrorCode.InternalServerError:
                        return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message });
                    default:
                        return StatusCode(500, new { message = "Неизвестная ошибка", details = ex.Message });
                }
            }
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegistrationGuest([FromBody] RegistrationGuestDto registrationGuestDto)
        {
            try
            {
                string result = await _authService.RegistrationGuest(registrationGuestDto);
                return Ok(new { message = result });
            }
            catch (ServiceException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("verify-client")]
        public async Task<IActionResult> ClientVerification([FromBody] VerificationDto verificationDto)
        {
            try
            {
                var isValid = await _authService.ClientVerification(verificationDto);
                if (isValid)
                {
                    return StatusCode(200, isValid);
                }
                else
                {
                    return StatusCode(401, isValid);
                }
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpPost("registration-employee")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> RegistrationEmployee(RegistrationEmployeeDto registrationEmployeeDto)
        {
            try
            {
                var result = await _authService.RegistrationEmployee(registrationEmployeeDto);
                return StatusCode(200, result);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }
    }
}

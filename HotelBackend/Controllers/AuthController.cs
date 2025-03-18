using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
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
    }
}

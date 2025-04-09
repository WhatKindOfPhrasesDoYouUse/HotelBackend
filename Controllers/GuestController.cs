using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Controllers
{
    [Route("api/guests")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService) => this._guestService = guestService;

        [HttpPatch("{clientId}/bind-card/{cardId}")]
        public async Task<IActionResult> BindCardToGuest(long clientId, long cardId)
        {
            if (clientId <= 0 || cardId <= 0)
            {
                return BadRequest("Id гостя и ID карты должны быть больше нуля.");
            }

            try
            {
                var updatedGuest = await _guestService.BindCardToGuest(clientId, cardId);
                return Ok(updatedGuest);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, new { error = "Внутренняя ошибка сервера" });
            }
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> GetGuestByClientId(long clientId)
        {
            try
            {
                var guest = await _guestService.GetGuestByClientId(clientId);
                return Ok(guest);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при получении данных гостя", details = ex.Message });
            }
        }
    }
}

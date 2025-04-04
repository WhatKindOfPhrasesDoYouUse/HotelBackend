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

        [HttpPatch("{guestId}/bind-card/{cardId}")]
        public async Task<IActionResult> BindCardToGuest(long guestId, long cardId)
        {
            if (guestId <= 0 || cardId <= 0)
            {
                return BadRequest("ID гостя и ID карты должны быть больше нуля.");
            }

            try
            {
                var updatedGuest = await _guestService.BindCardToGuest(guestId, cardId);
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

    }
}

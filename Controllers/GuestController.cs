using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{clientId:long}")]
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

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetGuests()
        {
            try
            {
                var guests = await _guestService.GetGuests();
                return StatusCode(200, guests);
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

        [HttpDelete("{guestId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteGuestById(long guestId)
        {
            try
            {
                await _guestService.DeleteGuestById(guestId);
                return StatusCode(200, "Гость успешно удален");
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

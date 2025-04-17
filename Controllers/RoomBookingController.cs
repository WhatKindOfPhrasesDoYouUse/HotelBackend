using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/room-bookings")]
    [ApiController]
    public class RoomBookingController : ControllerBase
    {
        private readonly IRoomBookingService _roomBookingService;

        public RoomBookingController(IRoomBookingService roomBookingService) => this._roomBookingService = roomBookingService;

        [HttpGet]
        public async Task<IActionResult> GetAllRoomBookings()
        {
            try
            {
                var roomBookings = await _roomBookingService.GetAllRoomBookings();
                return Ok(roomBookings);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new {message = ex.Message});
            }
        }

        [HttpGet("by-guest/{guestId:long}")]
        public async Task<IActionResult> GetRoomBookingsByGuestId(long guestId)
        {
            try
            {
                var roomBookings = await _roomBookingService.GetRoomBookingsByGuestId(guestId);
                return StatusCode(200, roomBookings);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new {message = ex.Message});
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("{guestId:long}/guest")]
        public async Task<IActionResult> GetDetailedRoomBookingByGuestId(long guestId)
        {
            try
            {
                var roomBookingDtos = await _roomBookingService.GetDetailedRoomBookingByGuestId(guestId);
                return StatusCode(200, roomBookingDtos);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpDelete("{bookingId:long}")]
        public async Task<IActionResult> DeleteBookingById(long bookingId)
        {
            try
            {
                await _roomBookingService.DeleteBookingById(bookingId);
                return StatusCode(200, "Бронирование успешно удалено");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveRoomBooking(RoomBooking roomBooking)
        {
            try
            {
                var resultRoomBooking = await _roomBookingService.SaveSingleRoomBooking(roomBooking);
                return StatusCode(200, roomBooking);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { mewssage = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при обработке запроса", details = ex.Message });
            }
        }

        [HttpPatch("{bookindId:long}/confirm")]
        public async Task<IActionResult> ConfirmSingleRoomBooking(long bookindId)
        {
            try
            {
                var booking = await _roomBookingService.ConfirmSingleRoomBooking(bookindId);
                return StatusCode(200, booking);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при обработке запроса", details = ex.Message });
            }
        }
    }
}

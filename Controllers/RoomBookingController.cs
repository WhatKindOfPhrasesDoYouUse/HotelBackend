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

        [HttpGet("by-guest/{guestId}")]
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

        [HttpPost]
        public async Task<IActionResult> SaveRoomBooking(RoomBooking roomBooking)
        {
            try
            {
                var resultRoomBooking = await _roomBookingService.SaveRoomBooking(roomBooking);
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
    }
}

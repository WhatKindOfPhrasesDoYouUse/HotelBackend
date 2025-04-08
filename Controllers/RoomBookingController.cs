using HotelBackend.Contracts;
using HotelBackend.Exceptions;
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
    }
}

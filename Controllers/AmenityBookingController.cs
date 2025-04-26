using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/amenity-bookings")]
    [ApiController]
    public class AmenityBookingController : ControllerBase
    {
        private readonly IAmenityBookingService _amenityBookingService;

        public AmenityBookingController(IAmenityBookingService amenityBookingService) => this._amenityBookingService = amenityBookingService;

        [HttpPost]
        public async Task<IActionResult> SaveAmenityBooking(AmenityBookingDto amenityBookingDto)
        {
            try
            {
                var amenityBooking = await _amenityBookingService.SaveAmenityBooking(amenityBookingDto);
                return StatusCode(200, amenityBooking);
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

        [HttpGet("{roomBookingId:long}/room-booking")]
        public async Task<IActionResult> GetAmenityBookings(long roomBookingId)
        {
            try
            {
                var amenityBookings = await _amenityBookingService.GetAmenityBookings(roomBookingId);
                return StatusCode(200, amenityBookings);
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

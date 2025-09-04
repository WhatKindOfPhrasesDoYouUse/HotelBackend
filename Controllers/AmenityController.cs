using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/amenities")]
    [ApiController]
    public class AmenityController : ControllerBase
    {
        private readonly IAmenityService _amenityService;

        public AmenityController(IAmenityService amenityService) => this._amenityService = amenityService;

        [HttpGet("{bookingId}/room-booking")]
        public async Task<IActionResult> GetAmenityByRoomBookingId(long bookingId)
        {
            try
            {
                var amenitys = await _amenityService.GetAmenityByRoomBookingId(bookingId);
                return StatusCode(200, amenitys);
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

        [HttpGet("{hotelId:long}/hotel")]
        public async Task<IActionResult> GetAllAmenitysByHotelId(long hotelId)
        {
            try
            {
                var amenitys = await _amenityService.GetAllAmenitysByHotelId(hotelId);
                return StatusCode(200, amenitys);
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

        [HttpGet("{amenityId:long}")]
        public async Task<IActionResult> GetAmenityById(long amenityId)
        {
            try
            {
                var amenity = await _amenityService.GetAmenityById(amenityId);
                return StatusCode(200, amenity);
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

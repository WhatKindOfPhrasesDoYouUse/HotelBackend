using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{roomBookingId:long}/details/room-booking")]
        public async Task<IActionResult> GetDetailAmenityBookingByBookingRoomId(long roomBookingId)
        {
            try
            {
                var amenityBookings = await _amenityBookingService.GetDetailAmenityBookingByBookingRoomId(roomBookingId);
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

        [HttpPatch("{amenityBookingId:long}/{employeeId:long}/take-amenity-task")]
        public async Task<IActionResult> TakeAmenityTask(long amenityBookingId, long employeeId)
        {
            try
            {
                var amenityBooking = await _amenityBookingService.TakeAmenityTask(amenityBookingId, employeeId);
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

        [HttpPatch("{amenityBookingId:long}/{employeeId:long}/done-amenity-task")]
        public async Task<IActionResult> DoneAmenityTask(long amenityBookingId, long employeeId)
        {
            try
            {
                var amenityBooking = await _amenityBookingService.DoneAmenityTask(amenityBookingId, employeeId);
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

        [HttpPatch("{amenityBookingId:long}/{guestId:long}/confirmation-amenity")]
        public async Task<IActionResult> ConfirmationAmenityFromGuest(long amenityBookingId, long guestId)
        {
            try
            {
                var amenityBooking = await _amenityBookingService.ConfirmationAmenityFromGuest(amenityBookingId, guestId);
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

        [HttpGet("{employeeTypeId:long}/tasks-by-employee-type")]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> GetAmenityBookingTasksByEmployeeTypeId(long employeeTypeId)
        {
            try
            {
                var amenityBookings = await _amenityBookingService.GetAmenityBookingTasksByEmployeeTypeId(employeeTypeId);
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

        [HttpGet("{employeeTypeId:long}/done-tasks-by-employee-type")]
        [Authorize(Roles = "employee")]
        public async Task<IActionResult> GetDoneAmenityBookingTasksByEmployeeTypeId(long employeeTypeId)
        {
            try
            {
                var amenityBookings = await _amenityBookingService.GetDoneAmenityBookingTasksByEmployeeTypeId(employeeTypeId);
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

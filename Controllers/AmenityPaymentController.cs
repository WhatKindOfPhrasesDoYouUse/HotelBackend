using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/amenity-payments")]
    [ApiController]
    public class AmenityPaymentController : ControllerBase
    {
        private readonly IAmenityPaymentService _amenityPaymentService;

        public AmenityPaymentController(IAmenityPaymentService amenityPaymentService) => this._amenityPaymentService = amenityPaymentService;

        [HttpPost]
        public async Task<IActionResult> SaveAmenityPayment(AmenityPaymentDto amenityPaymentDto)
        {
            try
            {
                var amenityPayment = await _amenityPaymentService.SaveAmenityPayment(amenityPaymentDto);
                return StatusCode(200, amenityPayment);
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAmenityPayments()
        {
            try
            {
                var amenityPayments = await _amenityPaymentService.GetAmenityPayments();
                return StatusCode(200, amenityPayments);
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

        [HttpDelete("{amenityPaymentId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAmenityPaymentById(long amenityPaymentId)
        {
            try
            {
                await _amenityPaymentService.DeleteAmenityPaymentById(amenityPaymentId);
                return StatusCode(200, "Платеж успешно удален");
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

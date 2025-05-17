using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/room-payments")]
    [ApiController]
    public class RoomPaymentController : ControllerBase
    {
        private readonly IRoomPaymentService _roomPaymentService;

        public RoomPaymentController(IRoomPaymentService roomPaymentService) => this._roomPaymentService = roomPaymentService;

        [HttpPost]
        public async Task<IActionResult> SaveRoomPayment(RoomPaymentDto roomPaymentDto)
        {
            try
            {
                await _roomPaymentService.SaveRoomPayment(roomPaymentDto);
                return StatusCode(200, "Оплату успешно сохранена");
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

        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetDetailsByBookingId(long bookingId)
        {
            try
            {
                var roomPaymentDetailsDto = await _roomPaymentService.GetDetailsByBookingId(bookingId);
                return StatusCode(200, roomPaymentDetailsDto);
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
        public async Task<IActionResult> GetRoomPayments()
        {
            try
            {
                var roomPayments = await _roomPaymentService.GetRoomPayments();
                return StatusCode(200, roomPayments);
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

        [HttpDelete("{roomPaymentId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteRoomPaymentById(long roomPaymentId)
        {
            try
            {
                await _roomPaymentService.DeleteRoomPaymentById(roomPaymentId);
                return StatusCode(200, "Платеж успешно обновлен");
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

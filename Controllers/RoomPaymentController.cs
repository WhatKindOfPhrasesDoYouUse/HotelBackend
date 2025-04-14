using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
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
    }
}

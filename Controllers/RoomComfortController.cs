using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/room-comforts")]
    [ApiController]
    public class RoomComfortController : ControllerBase
    {
        private readonly IRoomComfortService _roomComfortService;

        public RoomComfortController(IRoomComfortService roomComfortService) => this._roomComfortService = roomComfortService;

        [HttpGet]
        public async Task<IActionResult> GetRoomComforts()
        {
            try
            {
                var roomComforts = await _roomComfortService.GetRoomComforts();
                return StatusCode(200, roomComforts);
            }
            catch (ServiceException ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

        [HttpDelete("{roomId:long}/{comfortId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteRoomComfortByRoomAndComfortId(long roomId, long comfortId)
        {
            try
            {
                await _roomComfortService.DeleteRoomComfortByRoomAndComfortId(roomId, comfortId);
                return StatusCode(200, "Комфортабельность комнаты успешно удалена");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

        [HttpPost("{roomId:long}/{comfortId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveRoomComfort(long roomId, long comfortId)
        {
            try
            {
                await _roomComfortService.SaveRoomComfort(roomId, comfortId);
                return StatusCode(200, "Комфортабельность комнаты успешно создана");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

    }
}

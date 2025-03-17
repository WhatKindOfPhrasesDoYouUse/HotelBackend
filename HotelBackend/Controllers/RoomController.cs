using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService) => this._roomService = roomService;

        [HttpGet("hotel/{hotelId:long}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRoomsByHotelId(long hotelId)
        {
            try
            {
                var rooms = await _roomService.GetRoomsByHotelId(hotelId);
                return Ok(rooms);
            }
            catch (ServiceException ex)
            {
                switch (ex.ErrorCode) 
                {
                    case ErrorCode.NotFound:
                        return NotFound(new { message = ex.Message });
                    case ErrorCode.InternalServerError:
                        return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message });
                    default:
                        return StatusCode(500, new { message = "Неизвестная ошибка", details = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при обработке запроса", details = ex.Message });
            }
        }
        
    }
}

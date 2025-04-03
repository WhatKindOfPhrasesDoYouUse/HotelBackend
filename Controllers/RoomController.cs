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

        [HttpGet("sort/{hoteldId:long}")]
        public async Task<IActionResult> SortRooms(long hoteldId, bool? sortingDirectionByPrice, bool? sortingDirectionByCapacity)
        {
            try
            {
                var rooms = await _roomService.SortRooms(hoteldId, sortingDirectionByPrice, sortingDirectionByCapacity);
                return Ok(rooms);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Произошла ошибка сервера", details =ex.Message });
            }
        }

        [HttpGet("filter/{hotelId:long}")]
        public async Task<IActionResult> FilterRooms(long hotelId, [FromQuery] int? capacity, [FromQuery] int? minUnitPrice, [FromQuery] int? maxUnitPrice)
        {
            try
            {
                var rooms = await _roomService.FilterRooms(hotelId, capacity, minUnitPrice, maxUnitPrice);
                return Ok(rooms);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new {message  = ex.Message});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

        [HttpGet("{roomId:long}/comforts")]
        public async Task<IActionResult> GetComfortsByRoomId(long roomId)
        {
            try
            {
                var comforts = await _roomService.GetComfortsByRoomId(roomId);
                return Ok(comforts);
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

        [HttpGet("filter-by-comforts/{hotelId:long}")]
        public async Task<IActionResult> GetRoomsByComforts(long hotelId, [FromQuery] List<long> comfortIds)
        {
            try
            {
                var rooms = await _roomService.FilterRoomsByComforts(hotelId, comfortIds);
                return Ok(rooms);
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

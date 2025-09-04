using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/hotels")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelController(IHotelService hotelService) => this._hotelService = hotelService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> Hotels()
        {
            try
            {
                var hotels = await _hotelService.GetAllHotels();
                return Ok(hotels);
            }
            catch (ServiceException ex) 
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при обработке запроса", details = ex.Message });
            }
        }
    }
}

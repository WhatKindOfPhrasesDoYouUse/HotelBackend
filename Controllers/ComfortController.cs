using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/comforts")]
    [ApiController]
    public class ComfortController : ControllerBase
    {
        private readonly IComfortService _comfortService;

        public ComfortController(IComfortService comfortService) => this._comfortService = comfortService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comfort>>> GetAllComforts()
        {
            try
            {
                var comforts = await _comfortService.GetAllComforts();
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
    }
}

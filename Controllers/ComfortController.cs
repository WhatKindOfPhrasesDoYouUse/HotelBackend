using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpDelete("{comfortId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteComfortById(long comfortId)
        {
            try
            {
                await _comfortService.DeleteComfortById(comfortId);
                return StatusCode(200, "Комфорт успешно удален");
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

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveComfort(Comfort comfort)
        {
            try
            {
                await _comfortService.SaveComfort(comfort);
                return StatusCode(200, "Комфорт успешно добален");
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

        [HttpGet("{comfortId:long}")]
        public async Task<IActionResult> GetComfortById(long comfortId)
        {
            try
            {
                var comfort = await _comfortService.GetComfortById(comfortId);
                return StatusCode(200, comfort);
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

        [HttpPatch("{comfortId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateComfortById(long comfortId, Comfort newComfort)
        {
            try
            {
                await _comfortService.UpdateComfortById(comfortId, newComfort);
                return StatusCode(200, "Комфорт успешно обновлен");
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

using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using HotelBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/hotel-types")]
    [ApiController]
    public class HotelTypeController : ControllerBase
    {
        private readonly IHotelTypeService _hotelTypeService;

        public HotelTypeController(IHotelTypeService hotelTypeService) => this._hotelTypeService = hotelTypeService;

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetHotelTypes()
        {
            try
            {
                var hotelTypes = await _hotelTypeService.GetHotelTypes();
                return StatusCode(200, hotelTypes);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }

        [HttpGet("{hotelTypeId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetHotelTypeById(long hotelTypeId)
        {
            try
            {
                var hotelType = await _hotelTypeService.GetHotelTypeById(hotelTypeId);
                return StatusCode(200, hotelType);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }

        [HttpDelete("{hotelTypeId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteHotelTypeById(long hotelTypeId)
        {
            try
            {
                await _hotelTypeService.DeleteHotelTypeById(hotelTypeId);
                return StatusCode(200, "Тип отеля успешно удален");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveHotelType(HotelType hotelType)
        {
            try
            {
                await _hotelTypeService.SaveHotelType(hotelType);
                return StatusCode(201, "Тип гостиницы успешно создан");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new { error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }

        [HttpPatch("{hotelTypeId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateHotelTypeById(long hotelTypeId, HotelType newHotelType)
        {
            try
            {
                await _hotelTypeService.UpdateHotelTypeById(hotelTypeId, newHotelType);
                return StatusCode(202, "Тип гостиницы успешно обновлен");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Внутренняя ошибка сервера: {ex.Message}" });
            }
        }
    }
}

using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/employee-types")]
    [ApiController]
    public class EmployeeTypeController : ControllerBase
    {
        private readonly IEmployeeTypeService _employeeTypeService;

        public EmployeeTypeController(IEmployeeTypeService employeeTypeService) => this._employeeTypeService = employeeTypeService;

        [HttpGet]
        public async Task<IActionResult> GetEmployeeTypes()
        {
            try
            {
                var employeeTypes = await _employeeTypeService.GetEmployeeTypes();
                return StatusCode(200, employeeTypes);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

        [HttpGet("{employeeTypeId:long}")]
        public async Task<IActionResult> GetEmployeeTypeById(long employeeTypeId)
        {
            try
            {
                var employeeType = await _employeeTypeService.GetEmployeeTypeById(employeeTypeId);
                return StatusCode(200, employeeType);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

        [HttpDelete("{employeeTypeId:long}")]
        public async Task<IActionResult> DeleteEmployeeTypeById(long employeeTypeId)
        {
            try
            {
                await _employeeTypeService.DeleteEmployeeTypeById(employeeTypeId);
                return StatusCode(200, "Тип сотрудника успешно обновлен");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveEmployeeType(EmployeeType employeeType)
        {
            try
            {
                await _employeeTypeService.SaveEmployeeType(employeeType);
                return StatusCode(201, "Тип сотрудника успешно добавлен");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }

        [HttpPatch("{employeeTypeId:long}")]
        public async Task<IActionResult> UpdateEmployeeTypeById(long employeeTypeId, EmployeeType newEmployeeType)
        {
            try
            {
                await _employeeTypeService.UpdateEmployeeTypeById(employeeTypeId, newEmployeeType);
                return StatusCode(200, "Тип сотрудника успешно обновлен");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка сервера", details = ex.Message });
            }
        }
    }
}
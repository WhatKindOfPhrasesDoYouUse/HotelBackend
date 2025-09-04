using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService) => this._employeeService = employeeService;

        [HttpGet]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _employeeService.GetEmployees();
                return StatusCode(200, employees);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при получении данных гостя", details = ex.Message });
            }
        }

        [HttpDelete("{employeeId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteEmployeeById(long employeeId)
        {
            try
            {
                await _employeeService.DeleteEmployeeById(employeeId);
                return StatusCode(200, "Сотрудник успешно удален");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Произошла ошибка при получении данных гостя", details = ex.Message });
            }
        }

        [HttpGet("{employeeId:long}")]
        public async Task<IActionResult> GetEmployeeById(long employeeId)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeById(employeeId);
                return StatusCode(200, employee);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при получении данных гостя", details = ex.Message });
            }
        }

        [HttpPatch("{employeeId:long}")]
        [Authorize(Roles = "Administrator, employee")]
        public async Task<IActionResult> UpdateEmployeeById(long employeeId, Employee newEmployee)
        {
            try
            {
                await _employeeService.UpdateEmployeeById(employeeId, newEmployee);
                return StatusCode(200, "Сотрудник успешно обновлен");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при получении данных гостя", details = ex.Message });
            }
        }
    }
}

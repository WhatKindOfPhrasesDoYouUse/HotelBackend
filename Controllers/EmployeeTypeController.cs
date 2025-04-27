using HotelBackend.Contracts;
using HotelBackend.Exceptions;
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
    }
}

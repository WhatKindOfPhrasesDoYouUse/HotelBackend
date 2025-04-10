using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/banks")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IBankService _bankService;

        public BankController(IBankService bankService) => this._bankService = bankService;

        [HttpGet]
        public async Task<IActionResult> GetAllBanks()
        {
            try
            {
                var banks = await _bankService.GetAllBanks();
                return StatusCode(200, banks);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

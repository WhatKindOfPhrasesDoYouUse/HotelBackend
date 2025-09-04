using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using HotelBackend.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpDelete("{bankId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteBankById(long bankId)
        {
            try
            {
                await _bankService.DeleteBankById(bankId);
                return StatusCode(200, "Банк успешно удален");
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

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SaveBank(Bank bank)
        {
            try
            {
                await _bankService.SaveBank(bank);
                return StatusCode(201, "Банк успешно создан");
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

        [HttpGet("{bankId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetBankById(long bankId)
        {
            try
            {
                var bank = await _bankService.GetBankById(bankId);
                return StatusCode(200, bank);
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

        [HttpPatch("{bankId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdateHotelTypeById(long bankId, Bank newBank)
        {
            try
            {
                await _bankService.UpdateBankById(bankId, newBank);
                return StatusCode(202, "Банк успешно обновлен");
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

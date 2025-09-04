using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/payment-types")]
    [ApiController]
    public class PaymentTypeController : ControllerBase
    {
        private readonly IPaymentTypeService _paymentTypeService;

        public PaymentTypeController(IPaymentTypeService paymentTypeService) => this._paymentTypeService = paymentTypeService;

        [HttpGet]
        public async Task<IActionResult> GetAllPaymentTypes()
        {
            try
            {
                var paymentTypes = await _paymentTypeService.GetAllPaymentTypes();
                return StatusCode(200, paymentTypes);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SavePaymentType(PaymentType paymentType)
        {
            try
            {
                await _paymentTypeService.SavePaymentType(paymentType);
                return StatusCode(201, "Тип оплаты успешно создан");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpDelete("{paymentTypeId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeletePaymentTypeById(long paymentTypeId)
        {
            try
            {
                await _paymentTypeService.DeletePaymentTypeById(paymentTypeId);
                return StatusCode(200, "Тип оплаты успешно удален");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpGet("{paymentTypeId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetPaymentTypeById(long paymentTypeId)
        {
            try
            {
                var paymentType = await _paymentTypeService.GetPaymentTypeById(paymentTypeId);
                return StatusCode(200, paymentType);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }

        [HttpPatch("{paymentTypeId:long}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> UpdatePaymentTypeById(long paymentTypeId, PaymentType newPaymentType)
        {
            try
            {
                await _paymentTypeService.UpdatePaymentTypeById(paymentTypeId, newPaymentType);
                return StatusCode(200, "Тип оплаты успешно обновлен");
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла внутренняя ошибка сервера: {ex.Message}");
            }
        }
    }
}

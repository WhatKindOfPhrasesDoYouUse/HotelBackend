using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService) => this._clientService= clientService;

        [HttpGet("{clientId:long}/guest")]
        public async Task<IActionResult> GetGuestByClientId(long clientId)
        {
            try
            {
                var client = await _clientService.GetGuestByClientId(clientId);
                return Ok(client);
            }
            catch (ServiceException ex) 
            {
                switch (ex.ErrorCode)
                {
                    case ErrorCode.NotFound:
                        return NotFound(ex.Message);
                    case ErrorCode.InternalServerError:
                        return StatusCode(500, new { message = ex.Message, details = ex.InnerException?.Message });
                    default:
                        return StatusCode(500, new { message = "Неизвестная ошибка", details = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Произошла ошибка при обработке запроса", details = ex.Message });
            }
        }

        [HttpPatch("{clientId}")]
        public async Task<IActionResult> UpdateClient(long clientId, [FromBody] UpdateClientGuestDto updateClientGuestDto)
        {
            try
            {
                if (updateClientGuestDto == null)
                {
                    return BadRequest("Отсутствуют данные для обновления.");
                }

                var updatedClient = await _clientService.UpdateClientGuest(clientId, updateClientGuestDto);

                if (updatedClient == null)
                {
                    return NotFound($"Клиент с ID {clientId} не найден.");
                }

                return Ok(updatedClient);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Произошла ошибка при обновлении данных клиента.");
            }
        }
    }
}

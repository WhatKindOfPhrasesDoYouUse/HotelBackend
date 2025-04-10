using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Controllers
{
    [Route("api/cards")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService) => this._cardService = cardService;

        [HttpPost]
        public async Task<IActionResult> CreateCard([FromBody] Card card)
        {
            if (card == null)
            {
                return BadRequest("Данные карты не могут быть пустыми");
            }

            try
            {
                var createdCard = await _cardService.CreateCard(card);
                return CreatedAtAction(nameof(CreateCard), new { id = createdCard.Id }, createdCard);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (DbUpdateException)
            {
                throw new ServiceException(ErrorCode.DatabaseError, "Ошибка при сохранении данных");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
                return StatusCode(500, "Произошла ошибка на сервере");
            }
        }

        [HttpGet("{guestId}/guest")]
        public async Task<IActionResult> GetCardByGuestIdEndpoint(long guestId)
        {
            try
            {
                var card = await _cardService.GetCardByGuestId(guestId);
                return StatusCode(200, card);
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

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCardById(long cardId)
        {
            try
            {
                await _cardService.DeleteCardById(cardId);
                return StatusCode(200, "Карта успешно удалена");
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

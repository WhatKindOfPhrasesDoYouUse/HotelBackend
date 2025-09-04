using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "guest")]
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

        [HttpGet("{guestId:long}/guest")]
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

        [HttpDelete("{cardId:long}")]
        [Authorize(Roles = "Administrator, guest")]
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

        [HttpPatch("{cardId:long}")]
        public async Task<IActionResult> UpdateCard(long cardId, CardDto cardDto)
        {
            try
            {
                await _cardService.UpdateCard(cardId, cardDto);
                return StatusCode(200, "Данные карты успешно обновлены");
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

        [HttpGet("{cardId:long}")]
        public async Task<IActionResult> GetCardById(long cardId)
        {
            try
            {
                var card = await _cardService.GetCardById(cardId);
                return StatusCode(200, card);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка при получении данных карты: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetAllCards()
        {
            try
            {
                var cards = await _cardService.GetAllCards();
                return StatusCode(200, cards);
            }
            catch (ServiceException ex)
            {
                return StatusCode((int)ex.ErrorCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка при получении данных карты: {ex.Message}");
            }
        }
    }
}

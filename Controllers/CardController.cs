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
    }
}

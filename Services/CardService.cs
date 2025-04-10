using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class CardService : ICardService
    {
        private readonly ApplicationDbContext _context;

        public CardService(ApplicationDbContext context) => this._context = context;

        public async Task<Card> CreateCard(Card card)
        {
            try
            {
                var existingCard = await _context.Cards
                    .FirstOrDefaultAsync(c => c.GuestId == card.GuestId);

                if (existingCard != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, $"Гость с ID {card.GuestId} уже имеет привязанную карту");
                }

                var existingNumber = await _context.Cards
                    .FirstOrDefaultAsync(c => c.CardNumber == card.CardNumber);

                if (existingNumber != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Карта с таким номером уже существует");
                }

                var bank = await _context.Banks
                    .FirstOrDefaultAsync(b => b.Id == card.BankId);

                if (bank == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Банк с ID {card.BankId} не найден");
                }

                await _context.Cards.AddAsync(card);
                await _context.SaveChangesAsync();

                return card;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new ServiceException(ErrorCode.DatabaseError, "Ошибка при сохранении данных", ex);
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Внутренняя ошибка сервера", ex);
            }
        }

        public async Task<CardDto> GetCardByGuestId(long guestId)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть равно или меньше нуля");
                }

                if (await _context.Guests.FindAsync(guestId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {guestId} не найден");
                }

                var card = await _context.Cards.FirstOrDefaultAsync(c => c.GuestId == guestId);

                if (card == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Карта для гостя с id: {guestId} не найдена");
                }

                var bank = await _context.Banks.FirstOrDefaultAsync(b => b.Id == card.BankId);

                if (bank == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Банк картыid: {card.Id} не найдена");
                }

                CardDto cardDto = new CardDto
                {
                    Id = card.Id,
                    CardNumber = card.CardNumber,
                    CardDate = card.CardDate,
                    BankName = bank.Name
                };

                return cardDto;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new ServiceException(ErrorCode.InternalServerError, $"Произошла ошибка со стороны сервера");
            }
        }

        public async Task DeleteCardById(long cardId)
        {
            try
            {
                if (cardId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id карты не может быть равно или меньше нуля");
                }

                var card = await _context.Cards.FindAsync(cardId);

                if (card == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Карта с id: {cardId} не существует");
                }

                _context.Cards.Remove(card);

                await _context.SaveChangesAsync();
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, $"Произошла ошибка при удалении карты: {ex.Message}");
            }
        }
    }
}

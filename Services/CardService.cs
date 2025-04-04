using HotelBackend.Contracts;
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
                var existingCard = await _context.Cards.FirstOrDefaultAsync(c => c.Id == card.Id);

                if (existingCard != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Карта с таким номером уже существует");
                }

                var bank = await _context.Banks.FirstOrDefaultAsync(b => b.Id == card.BankId);

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
    }
}

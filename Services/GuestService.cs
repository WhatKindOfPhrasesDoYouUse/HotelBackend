using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace HotelBackend.Services
{
    public class GuestService : IGuestService
    {
        private readonly ApplicationDbContext _context;

        public GuestService(ApplicationDbContext context) => this._context = context;

        public async Task<Guest> BindCardToGuest(long guestId, long cardId)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть отрицательным значением");
                }

                if (cardId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id карты не может быть отрицательным значением");
                }

                var guest = await _context.Guests.FirstOrDefaultAsync(g => g.Id == guestId);

                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Гость с указанным id не найден");
                }

                var card = await _context.Cards.FirstOrDefaultAsync(g => g.Id == cardId);

                if (card == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Карта с указанным id не найден");
                }

                guest.DateOfBirth = guest.DateOfBirth.ToUniversalTime();

                guest.CardId = card.Id;
                guest.Card = card;

                _context.Guests.Update(guest);
                await _context.SaveChangesAsync();

                return guest;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при привязке карты к гостю.", ex);
            }
        }
    }
}

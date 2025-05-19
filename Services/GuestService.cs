using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class GuestService : IGuestService
    {
        private readonly ApplicationDbContext _context;

        public GuestService(ApplicationDbContext context) => this._context = context;


        public async Task<Guest> BindCardToGuest(long clientId, long cardId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "ID клиента не может быть отрицательным или нулевым.");
                }

                if (cardId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "ID карты не может быть отрицательным или нулевым.");
                }

                var guest = await _context.Guests
                    .FirstOrDefaultAsync(g => g.ClientId == clientId);

                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с ClientId {clientId} не найден.");
                }

                var card = await _context.Cards
                    .FirstOrDefaultAsync(c => c.Id == cardId);

                if (card == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Карта с ID {cardId} не найдена.");
                }

                if (guest.Card != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, $"Гость с ClientId {clientId} уже имеет привязанную карту.");
                }

                if (card.GuestId != 0 && card.GuestId != guest.Id)
                {
                    throw new ServiceException(ErrorCode.Conflict, $"Карта с ID {cardId} уже привязана к другому гостю.");
                }

                guest.DateOfBirth = guest.DateOfBirth.ToUniversalTime();

                guest.Card = card;
                card.GuestId = guest.Id;

                _context.Guests.Update(guest);
                _context.Cards.Update(card);

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

        public async Task<Guest> GetGuestByClientId(long clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id клиента не может быть равно нулю или отрицательному числу");
                }

                if (await _context.Clients.FirstOrDefaultAsync(c => c.Id == clientId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Клиент с id: {clientId} не найден");
                }

                var guest = await _context.Guests.FirstOrDefaultAsync(g => g.ClientId == clientId);

                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {guest.Id} не найден");
                }

                return guest;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении данных гостя", ex);
            }
        }

        public async Task<IEnumerable<Guest>> GetGuests()
        {
            try
            {
                var guests = await _context.Guests
                    .Include(c => c.Client)
                    .ToListAsync();

                if (guests == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Гости не найдены");
                }

                return guests;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteGuestById(long guestId)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id не может быть меньше или равно 0");
                }

                var guest = await _context.Guests
                    .Include(c => c.Client)
                    .Include(c => c.Card)
                    .FirstOrDefaultAsync(g => g.Id == guestId);

                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {guestId} не найден");
                }

                if (guest.Card != null) _context.Cards.Remove(guest.Card);

                _context.Clients.Remove(guest.Client);
                _context.Guests.Remove(guest);
                await _context.SaveChangesAsync();
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

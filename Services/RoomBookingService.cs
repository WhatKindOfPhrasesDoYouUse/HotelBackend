using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<RoomBooking>> GetAllRoomBookings()
        {
            try
            {
                var roomBookings = await _context.RoomBookings.ToListAsync();

                if (roomBookings == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Ссылка на бронирования не найдена");
                }

                return roomBookings;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка со стороны сервера при фильтрации данных", ex);
            }
        }

        public async Task<IEnumerable<RoomBooking>> GetRoomBookingsByGuestId(long guestId)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть меньше или равно нулю");
                }
                
                if (await _context.Guests.FindAsync(guestId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {guestId} не найден");
                }

                var roomBookings = await _context.RoomBookings.Where(rb => rb.GuestId == guestId)
                    .Include(g => g.Guest)
                    .Include(r => r.Room)
                    .ToListAsync();

                if (roomBookings == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Ссылка на бронирования гостя с id: {guestId} не найдена");
                }

                return roomBookings;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, $"Произошла ошибка со стороны сервера при получении бронирований комнат гостя с id: {guestId}", ex);
            }
        }
    }
}

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
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка со стороны сервера при получении данных бронирований комнат", ex);
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

        public async Task<RoomBooking> SaveRoomBooking(RoomBooking roomBooking)
        {
            try
            {
                if (roomBooking.GuestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть меньше или равен 0");
                }

                if (await _context.Guests.FindAsync(roomBooking.GuestId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {roomBooking.GuestId} не найден");
                }

                if (roomBooking.RoomId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id комнаты не может быть меньше или равно нулю");
                }

                if (await _context.Rooms.FindAsync(roomBooking.RoomId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomBooking.RoomId} не найден");
                }

                if (roomBooking.CheckInDate.ToDateTime(roomBooking.CheckInTime) >= roomBooking.CheckOutDate.ToDateTime(roomBooking.CheckOutTime))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Дата и время заезда должны быть позде даты и времени бронирования");
                }

                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomBooking.RoomId);

                if (roomBooking.NumberOfGuests > room.Capacity)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"Количество желаемых мест: {roomBooking.NumberOfGuests} должно быть меньше {room.Capacity}");
                }

                bool isOverlappingBookingExists = await _context.RoomBookings.
                    AnyAsync(rb => rb.RoomId == roomBooking.RoomId
                    && rb.Id != roomBooking.Id
                    && roomBooking.CheckInDate <= rb.CheckOutDate && roomBooking.CheckOutDate >= rb.CheckInDate);

                if (isOverlappingBookingExists)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Комната уже забронирована на указанный период");
                }

                _context.RoomBookings.Add(roomBooking);
                await _context.SaveChangesAsync();

                return roomBooking;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, $"Произошла ошибка со стороны сервера при создании бронирования: {ex}");
            }
        } 
    }
}

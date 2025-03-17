using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationDbContext _context;

        public RoomService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<Room>> GetRoomsByHotelId(long hotelId)
        {
            try
            {
                var rooms = await _context.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();

                if (rooms == null || rooms.Count == 0)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Комнаты не найдены");
                }

                return rooms;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении комнат", ex);
            }
        }
    }
}

using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class RoomComfortService : IRoomComfortService
    {
        private readonly ApplicationDbContext _context;

        public RoomComfortService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<RoomComfort>> GetRoomComforts()
        {
            try
            {
                var roomComforts = await _context.RoomsComforts
                    .Include(r => r.Room)
                    .Include(c => c.Comfort)
                    .ToListAsync();

                if (roomComforts == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Список комфортабельности комнат не найден");
                }

                return roomComforts;
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

        public async Task DeleteRoomComfortByRoomAndComfortId(long roomId, long comfortId)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(roomId);
                var comfort = await _context.Comforts.FindAsync(comfortId);

                if (room == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomId} не найдена");
                }

                if (comfort == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комфорт с id: {comfortId} не найден");
                }

                var roomComfort = await _context.RoomsComforts.FirstOrDefaultAsync(rc => rc.RoomId == roomId && rc.ComfortId == comfortId);

                if (roomComfort == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Такая комфортабельность комнаты не найдена");
                }

                _context.RoomsComforts.Remove(roomComfort);
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

        public async Task SaveRoomComfort(long roomId, long comfortId)
        {
            try
            {
                var room = await _context.Rooms.FindAsync(roomId);
                var comfort = await _context.Comforts.FindAsync(comfortId);

                if (room == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomId} не найдена");
                }

                if (comfort == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomId} не найдена");
                }

                var existingRoomComfort = await _context.RoomsComforts.FirstOrDefaultAsync(rc => rc.RoomId == roomId && rc.ComfortId == comfortId);

                if (existingRoomComfort != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Такая связка уже существует");
                }

                RoomComfort roomComfort = new RoomComfort
                {
                    RoomId = roomId,
                    ComfortId = comfortId
                };

                await _context.RoomsComforts.AddAsync(roomComfort);
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

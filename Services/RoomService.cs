using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
                var rooms = await _context.Rooms
                    .Where(r => r.HotelId == hotelId)
                    .ToListAsync();
                
                if (rooms == null || rooms.Count == 0)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Комнаты не найдены");
                }

                return rooms;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении комнат со стороны сервера", ex);
            }
        }

        public async Task<IEnumerable<Room>> SortRooms(long hotelId, bool? sortingDirectionByPrice, bool? sortingDirectionByCapacity)
        {
            try
            {
                if (hotelId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id отеля не может быть отрицательным значением");
                }

                var rooms = await _context.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();

                if (rooms == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комнаты для отеля с id: {hotelId} не найдены");
                }

                if (sortingDirectionByPrice.HasValue)
                {
                    rooms = sortingDirectionByPrice.Value
                        ? rooms.OrderBy(r => r.UnitPrice).ToList()
                        : rooms.OrderByDescending(r => r.UnitPrice).ToList();
                }

                if (sortingDirectionByCapacity.HasValue)
                {
                    rooms = sortingDirectionByCapacity.Value
                        ? rooms.OrderBy(r => r.UnitPrice).ToList()
                        : rooms.OrderByDescending(r => r.UnitPrice).ToList();
                }

                return rooms;
            } 
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка со стороны сервера при сортировке данных", ex);
            }
        }

        public async Task<IEnumerable<Room>> FilterRooms(long hotelId, int? capacity, int? minUnitPrice, int? maxUnitPrice)
        {
            try
            {
                if (hotelId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id отеля не может быть отрицательным значением");
                }

                var query = _context.Rooms.Where(r => r.HotelId == hotelId).AsQueryable();

                if (query == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комнаты для отеля с id: {hotelId} не найдены");
                }

                if (minUnitPrice.HasValue)
                {
                    query = query.Where(p => p.UnitPrice >= minUnitPrice.Value);
                }

                if (maxUnitPrice.HasValue)
                {
                    query = query.Where(p => p.UnitPrice <= maxUnitPrice.Value);
                }

                if (minUnitPrice.HasValue && maxUnitPrice.HasValue)
                {
                    query = query.Where(r => r.UnitPrice >= minUnitPrice && r.UnitPrice <= maxUnitPrice);
                }

                if (capacity.HasValue)
                {
                    query = query.Where(r => r.Capacity >= capacity);
                }

                return query.ToList();
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

        public async Task<IEnumerable<Comfort>> GetComfortsByRoomId(long roomId) 
        {
            try
            {
                if (roomId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id комнаты: {roomId} не может быть отрицательным числом");
                }

                var comforts = await _context.RoomsComforts
                    .Where(rc => rc.RoomId == roomId)
                    .Include(rc => rc.Comfort)
                    .Select(rc => rc.Comfort)
                    .ToListAsync();

                if (comforts == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комфортности для комнаты с id: {roomId} не найдены");
                }

                return comforts;
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

        public async Task<IEnumerable<Room>> FilterRoomsByComforts(long hotelId, List<long>? comfortIds)
        {
            try
            {
                if (hotelId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id отеля: {hotelId} не может быть отрицательным числом");
                }

                return await _context.Rooms.Where(room => room.RoomComforts.Any(rc => comfortIds.Contains(rc.ComfortId)))
                    .ToListAsync();
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

        public async Task<Room> GetRoomById(long roomId)
        {
            try
            {
                if (roomId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id комнаты не может быть меньше или равно 0");
                }

                var room = await _context.Rooms.FindAsync(roomId);

                if (room == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomId} не найдена");
                }

                return room;
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

        public async Task<bool> HasRoomIsAvailable(long roomId)
        {
            try
            {
                if (roomId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id комнаты не может быть меньше или равно 0");
                }

                var room = await _context.Rooms
                    .Include(r => r.RoomBookings)
                    .FirstOrDefaultAsync(r => r.Id == roomId);

                if (room == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomId} не найдена");
                }

                var today = DateOnly.FromDateTime(DateTime.UtcNow);

                bool isOccupied = room.RoomBookings.Any(rb => rb.CheckInDate <= today && rb.CheckOutDate >= today);

                return !isOccupied;
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

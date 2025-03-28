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
    }
}

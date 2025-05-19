using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomsByHotelId(long hotelId);
        Task<IEnumerable<Room>> SortRooms(long hotelId, bool? sortingDirectionByPrice, bool? sortingDirectionByCapacity);
        Task<IEnumerable<Room>> FilterRooms(long hotelId, int? capacity, int? minUnitPrice, int? maxUnitPrice);
        Task<IEnumerable<Comfort>> GetComfortsByRoomId(long roomId);
        Task<IEnumerable<Room>> FilterRoomsByComforts(long hotelId, List<long>? comfortIds);
        Task<Room> GetRoomById(long roomId);
        Task<bool> HasRoomIsAvailable(long roomId);
        Task<DateOnly> GetNextAvailableDate(long roomId);
        Task DeleteRoomById(long roomId);
        Task SaveRoom(Room room);
        Task UpdateRoomById(long roomId, UpdateRoomDto roomDto);
    }
}

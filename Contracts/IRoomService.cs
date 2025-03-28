using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetRoomsByHotelId(long hotelId);
        Task<IEnumerable<Room>> SortRooms(long hotelId, bool? sortingDirectionByPrice, bool? sortingDirectionByCapacity);
    }
}

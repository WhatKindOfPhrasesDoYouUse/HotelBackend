using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IRoomComfortService
    {
        Task<IEnumerable<RoomComfort>> GetRoomComforts();
        Task DeleteRoomComfortByRoomAndComfortId(long roomId, long comfortId);
        Task SaveRoomComfort(long roomId, long comfortId);
    }
}

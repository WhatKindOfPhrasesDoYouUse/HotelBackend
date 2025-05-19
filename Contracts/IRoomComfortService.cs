using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IRoomComfortService
    {
        Task<IEnumerable<RoomComfort>> GetRoomComforts();
        Task DeleteRoomComfortByRoomAndComfortId(long roomId, long comfortId);
        Task SaveRoomComfort(long roomId, long comfortId);
        Task UpdateRoomComfort(long roomId, long oldComfortId, long newComfortId);
        Task<RoomComfort> GetRoomComfort(long roomId, long comfortId);
    }
}

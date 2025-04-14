using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IRoomBookingService
    {
        Task<IEnumerable<RoomBooking>> GetAllRoomBookings();
        Task<IEnumerable<RoomBooking>> GetRoomBookingsByGuestId(long guestId);
        Task<IEnumerable<RoomBookingDto>> GetDetailedRoomBookingByGuestId(long guestId);
        Task DeleteBookingById(long bookindId);
        Task<RoomBooking> SaveRoomBooking(RoomBooking roomBooking);
    }
}

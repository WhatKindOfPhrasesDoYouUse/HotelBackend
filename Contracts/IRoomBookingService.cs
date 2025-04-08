using HotelBackend.Exceptions;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IRoomBookingService
    {
        Task<IEnumerable<RoomBooking>> GetAllRoomBookings();
        Task<IEnumerable<RoomBooking>> GetRoomBookingsByGuestId(long guestId);
        /*Task<RoomBooking> SaveRoomBooking(RoomBooking roomBooking);*/
    }
}

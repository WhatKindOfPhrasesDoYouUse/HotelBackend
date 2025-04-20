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
        Task<RoomBooking> SaveSingleRoomBooking(RoomBooking roomBooking);
        Task<RoomBooking> SaveGroupRoomBooking(RoomBooking roomBooking);
        Task<RoomBooking> ConfirmSingleRoomBooking(long bookingId);
        Task UpdateSingleRoomBookingId(long bookingId, UpdateRoomBookingDto updateRoomBookingDto);
        Task UpdateAdditionalGuestByRoomBookingId(long bookindId, List<AdditionalGuest> additionalGuest);
    }
}

using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IAmenityService
    {
        Task<IEnumerable<Amenity>> GetAmenityByRoomBookingId(long bookingId);
    }
}

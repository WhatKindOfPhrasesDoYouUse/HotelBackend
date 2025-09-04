using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IAmenityService
    {
        Task<IEnumerable<Amenity>> GetAmenityByRoomBookingId(long bookingId);
        Task<IEnumerable<Amenity>> GetAllAmenitysByHotelId(long hotelId);
        Task<Amenity> GetAmenityById(long amenityId);
    }
}

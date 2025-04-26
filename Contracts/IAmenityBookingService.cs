using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IAmenityBookingService
    {
        Task<AmenityBooking> SaveAmenityBooking(AmenityBookingDto amenityBookingDto);
        Task<IEnumerable<AmenityBooking>> GetAmenityBookings(long bookindRoomId);
    }
}

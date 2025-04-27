using HotelBackend.DataTransferObjects;
using HotelBackend.Models;
using System.Threading.Tasks;

namespace HotelBackend.Contracts
{
    public interface IAmenityBookingService
    {
        Task<AmenityBooking> SaveAmenityBooking(AmenityBookingDto amenityBookingDto);
        Task<IEnumerable<AmenityBooking>> GetAmenityBookings(long bookindRoomId);
        Task<AmenityBooking> TakeAmenityTask(long amenityBookingId, long employeeId);
        Task<AmenityBooking> DoneAmenityTask(long amenityBookingId, long employeeId);
        Task<AmenityBooking> ConfirmationAmenityFromGuest(long amenityBookingId, long guestId);
        Task<IEnumerable<AmenityBooking>> GetAmenityBookingTasksByEmployeeTypeId(long employeeTypeId);
        Task<IEnumerable<DoneAmenityBookingDto>> GetDoneAmenityBookingTasksByEmployeeTypeId(long employeeTypeId);
    }
}

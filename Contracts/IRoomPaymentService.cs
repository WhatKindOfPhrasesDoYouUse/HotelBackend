using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IRoomPaymentService
    {
        Task<RoomPayment> SaveRoomPayment(RoomPaymentDto roomPaymentDto);
        Task<RoomPaymentDetailsDto> GetDetailsByBookingId(long bookingId);
        Task<IEnumerable<RoomPayment>> GetRoomPayments();
        Task DeleteRoomPaymentById(long roomPaymentId);
    }
}

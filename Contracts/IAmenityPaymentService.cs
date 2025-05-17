using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IAmenityPaymentService
    {
        Task<AmenityPayment> SaveAmenityPayment(AmenityPaymentDto amenityPaymentDto);
        Task<IEnumerable<AmenityPayment>> GetAmenityPayments();
        Task DeleteAmenityPaymentById(long amenityPaymentId);
    }
}

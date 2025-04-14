using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IPaymentTypeService
    {
        Task<IEnumerable<PaymentType>> GetAllPaymentTypes();
    }
}

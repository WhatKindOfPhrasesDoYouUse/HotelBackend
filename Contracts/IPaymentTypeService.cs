using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IPaymentTypeService
    {
        Task<IEnumerable<PaymentType>> GetAllPaymentTypes();
        Task SavePaymentType(PaymentType paymentType);
        Task DeletePaymentTypeById(long paymentTypeId);
        Task<PaymentType> GetPaymentTypeById(long paymentTypeId);
        Task UpdatePaymentTypeById(long paymentTypeId, PaymentType newPaymentType);
    }
}

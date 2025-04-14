using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly ApplicationDbContext _context;

        public PaymentTypeService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<PaymentType>> GetAllPaymentTypes()
        {
            try
            {
                var paymentTypes = await _context.PaymentTypes.ToListAsync();

                if (paymentTypes == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Список типов оплаты не найдены");
                }

                return paymentTypes;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

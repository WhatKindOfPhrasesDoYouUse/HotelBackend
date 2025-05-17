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

        public async Task SavePaymentType(PaymentType paymentType)
        {
            try
            {
                var existingPaymentType = await _context.PaymentTypes.FirstOrDefaultAsync(pt => pt.Name == paymentType.Name);

                if (existingPaymentType != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Такой тип оплаты уже суещсвутет");
                }

                await _context.PaymentTypes.AddAsync(paymentType);
                await _context.SaveChangesAsync();
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

        public async Task DeletePaymentTypeById(long paymentTypeId)
        {
            try
            {
                if (paymentTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id типа оплаты не может быть меньше или равно 0");
                }

                var paymentType = await _context.PaymentTypes
                    .Include(rp => rp.RoomPayments)
                    .Include(ap => ap.AmenityPayments)
                    .FirstOrDefaultAsync(pt => pt.Id == paymentTypeId);

                if (paymentType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип оплаты с id: {paymentTypeId} не найдены");
                }
                
                _context.RoomPayments.RemoveRange(paymentType.RoomPayments);
                _context.AmenityPayments.RemoveRange(paymentType.AmenityPayments);

                _context.PaymentTypes.Remove(paymentType);
                await _context.SaveChangesAsync();
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

        public async Task<PaymentType> GetPaymentTypeById(long paymentTypeId)
        {
            try
            {
                if (paymentTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id типа оплаты не может быть меньше или равно 0");
                }

                var paymentType = await _context.PaymentTypes.FindAsync(paymentTypeId);

                if (paymentType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип оплаты с id: {paymentTypeId} не найдены");
                }

                return paymentType;
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

        public async Task UpdatePaymentTypeById(long paymentTypeId, PaymentType newPaymentType)
        {
            try
            {
                var paymentType = await _context.PaymentTypes.FindAsync(paymentTypeId);

                if (paymentType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип оплаты с id: {paymentTypeId} не найден");
                }

                if (!string.IsNullOrEmpty(newPaymentType.Name))
                    paymentType.Name = newPaymentType.Name;


                _context.PaymentTypes.Update(paymentType);
                await _context.SaveChangesAsync();
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

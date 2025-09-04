using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class AmenityPaymentService : IAmenityPaymentService
    {
        private readonly ApplicationDbContext _context;

        public AmenityPaymentService(ApplicationDbContext context) => this._context = context;

        private decimal CalculateBookingCost(AmenityBooking amenityBooking)
        {
            var amenity =  _context.Amenities.FirstOrDefault(a => a.Id == amenityBooking.AmenityId);

            if (amenity == null)
            {
                return 0;
            }

            return amenity.UnitPrice * amenityBooking.Quantity;
        }

        public async Task<AmenityPayment> SaveAmenityPayment(AmenityPaymentDto amenityPaymentDto)
        {
            try
            {
                if (amenityPaymentDto.AmenityBookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id заказанной услуги не может быть меньше или равно 0");
                }

                if (await _context.AmenityPayments.FirstOrDefaultAsync(ap => ap.AmenityBookingId == amenityPaymentDto.AmenityBookingId) != null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"Оплата заказанной услуги ${amenityPaymentDto.AmenityBookingId} уже существует");
                }

                var amenityBooking = await _context.AmenityBookings
                    .Include(a => a.Amenity)
                    .FirstOrDefaultAsync(ab => ab.Id == amenityPaymentDto.AmenityBookingId);

                if (amenityBooking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Заказанная услуга с id: {amenityPaymentDto.AmenityBookingId} не существует");
                }

                if (amenityPaymentDto.PaymentTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id типа оплаты не может быть меньше или равно 0");
                }

                var paymentType = await _context.PaymentTypes.FirstOrDefaultAsync(pt => pt.Id == amenityPaymentDto.PaymentTypeId);

                if (paymentType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Типа оплаты с id: {amenityPaymentDto.AmenityBookingId} не существует");
                }

                AmenityPayment amenityPayment = new AmenityPayment
                {
                    AmenityBookingId = amenityBooking.Id,
                    PaymentTypeId = paymentType.Id,
                    TotalAmount = CalculateBookingCost(amenityBooking),
                    PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                    PaymentTime = TimeOnly.FromDateTime(DateTime.Now),
                    PaymentStatus = "Оплачено"
                };

                _context.AmenityPayments.Add(amenityPayment);
                await _context.SaveChangesAsync();

                return amenityPayment;
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

        public async Task<IEnumerable<AmenityPayment>> GetAmenityPayments()
        {
            try
            {
                var amenityPayments = await _context.AmenityPayments
                    .Include(rb => rb.PaymentType)
                    .ToListAsync();

                if (amenityPayments == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Данные оплаты комнат не найдены");
                }

                return amenityPayments;
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

        public async Task DeleteAmenityPaymentById(long amenityPaymentId)
        {
            try
            {
                if (amenityPaymentId <= 0)
                {
                    throw new ServiceException(ErrorCode.NotFound, "id платежы услуги не может быть меньше или равно 0");
                }

                var amenityPayment = await _context.AmenityPayments.FindAsync(amenityPaymentId);

                if (amenityPayment == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Платеж услуги с id: {amenityPaymentId} не найден");
                }

                _context.AmenityPayments.Remove(amenityPayment);
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

using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class RoomPaymentService : IRoomPaymentService
    {
        private readonly ApplicationDbContext _context;

        public RoomPaymentService(ApplicationDbContext context) => this._context = context;

        private decimal CalculateBookingCost(RoomBooking roomBooking)
        {
            if (roomBooking == null)
            {
                throw new ArgumentNullException(nameof(roomBooking), "Бронирование не может быть null");
            }

            if (roomBooking.Room == null)
            {
                throw new InvalidOperationException("Информация о номере не загружена");
            }

            DateTime checkIn = roomBooking.CheckInDate.ToDateTime(roomBooking.CheckInTime);
            DateTime checkOut = roomBooking.CheckOutDate.ToDateTime(roomBooking.CheckOutTime);

            if (checkOut <= checkIn)
            {
                throw new InvalidOperationException("Дата выезда должна быть позже даты заезда");
            }

            double totalHours = (checkOut - checkIn).TotalHours;
            int nights = (int)Math.Ceiling(totalHours / 24);

            return nights * roomBooking.Room.UnitPrice;
        }

        public async Task<RoomPayment> SaveRoomPayment(RoomPaymentDto roomPaymentDto)
        {
            try
            {
                if (roomPaymentDto.RoomBookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id бронирования не может быть меньше или равно 0");
                }
                
                if (await _context.RoomPayments.FirstOrDefaultAsync(rp => rp.RoomBookingId == roomPaymentDto.RoomBookingId) != null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"Оплата для бронирования с id: {roomPaymentDto.RoomBookingId} уже существует");
                }

                var booking = await _context.RoomBookings
                    .Include(b => b.Room)
                    .FirstOrDefaultAsync(b => b.Id == roomPaymentDto.RoomBookingId);

                if (booking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Забронированная комната не найдена");
                }

                if (roomPaymentDto.PaymentTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id типа оплаты не может быть меньше или равно 0");
                }

                var paymentType = await _context.PaymentTypes.FirstOrDefaultAsync(pt => pt.Id == roomPaymentDto.PaymentTypeId);

                if (paymentType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип платежа не найден");
                }

                RoomPayment roomPayment = new RoomPayment
                {
                    RoomBookingId = booking.Id,
                    PaymentTypeId = paymentType.Id,
                    TotalAmount = CalculateBookingCost(booking),
                    PaymentDate = DateOnly.FromDateTime(DateTime.Now),
                    PaymentTime = TimeOnly.FromDateTime(DateTime.Now),
                    PaymentStatus = "Оплачено"
                };

                _context.RoomPayments.Add(roomPayment);
                await _context.SaveChangesAsync();

                return roomPayment;
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

using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class AmenityBookingService : IAmenityBookingService
    {
        private readonly ApplicationDbContext _context;

        public AmenityBookingService(ApplicationDbContext context) => this._context = context;

        // TODO: сделать проверку на попадание в даты бронирования
        public async Task<AmenityBooking> SaveAmenityBooking(AmenityBookingDto amenityBookingDto)
        {
            try
            {
                if (amenityBookingDto.AmenityId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id дополнительной услуги не может быть меньше или равно нулю");
                }

                var amenity = await _context.Amenities.FindAsync(amenityBookingDto.AmenityId);

                if (amenity == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Дополнительная услуга с id: {amenityBookingDto.AmenityId} не найдена");
                }

                if (amenityBookingDto.GuestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть меньше или равно нулю");
                }

                var guest = await _context.Guests.FindAsync(amenityBookingDto.GuestId);

                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"Гость с id: {amenityBookingDto.GuestId} не найден");
                }

                if (amenityBookingDto.Quantity <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Количество заказанных дополнительных услуг не может быть меньше или равно нулю");
                }

                var roomBookings = await _context.RoomBookings.Where(rb => rb.GuestId == amenityBookingDto.GuestId).ToListAsync();

                if (roomBookings == null || !roomBookings.Any())
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Для гостя с id: {amenityBookingDto.GuestId} забронированный комнаты не найдены");
                }

                var booking = new AmenityBooking
                {
                    AmenityId = amenityBookingDto.AmenityId,
                    GuestId = amenityBookingDto.GuestId,
                    Quantity = amenityBookingDto.Quantity,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    OrderTime = TimeOnly.FromDateTime(DateTime.Now),
                    ReadyDate = null,
                    ReadyTime = null,
                    CompletionStatus = "В ожидании подтверждения"
                };

                _context.AmenityBookings.Add(booking);
                await _context.SaveChangesAsync();

                return booking;
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

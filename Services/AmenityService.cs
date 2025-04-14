using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly ApplicationDbContext _context;

        public AmenityService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<Amenity>> GetAmenityByRoomBookingId(long bookingId)
        {
            try
            {
                if (bookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id бронирования не может быть меньше или равно 0");
                }

                var booking = await _context.Amenities.FirstOrDefaultAsync(b => b.Id == bookingId);

                if (booking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Забронированная комната не найдена");
                }

                var amenities = await _context.Amenities.Where(a => a.RoomId == booking.RoomId).ToListAsync();

                if (amenities == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Дополнительные услуги не найдены");
                }

                return amenities;
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

using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class HotelService : IHotelService
    {
        private readonly ApplicationDbContext _context;

        public HotelService(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Hotel>> GetAllHotels()
        {
            try
            {
                var hotels = await _context.Hotels
                    .Include(h => h.HotelType)
                        .ThenInclude(ht => ht.Hotels)
                    .Include(h => h.Employees)
                    .Include(h => h.HotelReviews)
                    .Include(h => h.Rooms)
                    .ToListAsync();

                if (hotels == null || hotels.Count == 0)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Отели не найдены");
                }

                return hotels;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении отелей со стороны сервера", ex);
            }
        }
    }
}

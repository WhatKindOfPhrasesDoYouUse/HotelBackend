using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class HotelReviewService : IHotelReviewService
    {
        private readonly ApplicationDbContext _context;
        
        public HotelReviewService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<HotelReview>> GetHotelReviews()
        {
            try
            {
                var hotelReviews = await _context.HotelReviews
                    .Include(hr => hr.Guest)
                    .Include(hr => hr.Hotel)
                    .ToListAsync();

                if (hotelReviews == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Отзывы об отеле отсутствуют");
                }

                return hotelReviews;
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

        public async Task<HotelReview> GetHotelReviewByGuestId(long guestId)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не должно быть меньше или равно 0");
                }

                var hotelReview = await _context.HotelReviews.FirstOrDefaultAsync(hr => hr.GuestId == guestId);

                if (hotelReview == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Отзывы гостя с id: {guestId} не найдены");
                }

                return hotelReview;
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

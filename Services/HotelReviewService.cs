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

/*        public async Task SaveHotelReview(HotelReview hotelReview)
        {
            try
            {
                if (hotelReview.GuestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "ID гостя не должно быть меньше или равно 0");
                }

                var guest = await _context.Guests.FindAsync(hotelReview.GuestId);
                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с ID {hotelReview.GuestId} не найден");
                }

                if (hotelReview.HotelId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "ID отеля не должно быть меньше или равно 0");
                }

                var hotel = await _context.Hotels.FindAsync(hotelReview.HotelId);
                if (hotel == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Отель с ID {hotelReview.HotelId} не найден");
                }

                // Получаем все бронирования гостя в этом отеле с платежами
                var completedBookings = await _context.RoomBookings
                    .Where(rb => rb.GuestId == hotelReview.GuestId && rb.Room.HotelId == hotelReview.HotelId)
                    .Include(rb => rb.RoomPayments)
                    .ToListAsync();

                if (!completedBookings.Any(rb => rb.RoomPayments.Any()))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость не имеет завершенных бронирований в этом отеле");
                }

                // Проверка, не оставлял ли гость уже отзыв по каждому завершенному бронированию
                var reviewedBookingIds = await _context.HotelReviews
                    .Where(hr => hr.GuestId == hotelReview.GuestId && hr.HotelId == hotelReview.HotelId)
                    .Select(hr => hr.RoomBookingId)
                    .ToListAsync();

                var unreviewedCompletedBooking = completedBookings
                    .Where(rb => rb.RoomPayments.Any() && !reviewedBookingIds.Contains(rb.Id))
                    .FirstOrDefault();

                if (unreviewedCompletedBooking == null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость уже оставил отзыв по всем завершенным бронированиям в этом отеле");
                }

                // Привязываем отзыв к одному из незаревьювленных завершенных бронирований

                await _context.HotelReviews.AddAsync(hotelReview);
                await _context.SaveChangesAsync();
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Можно логировать ошибку
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при сохранении отзыва", ex);
            }
        }*/
    }
}

using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class AmenityReviewService : IAmenityReviewService
    {
        private readonly ApplicationDbContext _context;

        public AmenityReviewService(ApplicationDbContext context) => this._context = context;

        public async Task<AmenityReview> SaveAmenityReview(AmenityReview amenityReview)
        {
            try
            {
                if (amenityReview.GuestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "ID гостя не должно быть меньше или равно 0");
                }

                var guest = await _context.Guests.FindAsync(amenityReview.GuestId);

                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с ID {amenityReview.GuestId} не найден");
                }

                if (amenityReview.AmenityId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "ID услуги не должно быть меньше или равно 0");
                }

                var amenity = await _context.Amenities.FindAsync(amenityReview.AmenityId);

                if (amenity == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Услуга с ID {amenityReview.AmenityId} не найдена");
                }

                var completedAmenityBookings = await _context.AmenityBookings
                    .Where(ab => ab.GuestId == amenityReview.GuestId && ab.AmenityId == amenityReview.AmenityId && ab.CompletionStatus == "Принята")
                    .Include(ab => ab.AmenityPayments)
                    .ToListAsync();

                if (!completedAmenityBookings.Any(ab => ab.AmenityPayments.Any(p => p.PaymentStatus == "Оплачено")))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость не имеет оплаченных и завершённых бронирований для этой услуги");
                }

                if (amenityReview.Rating < 1 || amenityReview.Rating > 5)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Рейтинг должен быть в интервале от 1 до 5");
                }

                var existingReview = await _context.AmenityReviews
                    .FirstOrDefaultAsync(ar => ar.GuestId == amenityReview.GuestId && ar.AmenityId == amenityReview.AmenityId && ar.AmenityBookingId == amenityReview.AmenityBookingId);

                if (existingReview != null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость уже оставил отзыв для данной услуги");
                }

                var unreviewedCompletedBooking = completedAmenityBookings
                    .FirstOrDefault(ab => ab.AmenityPayments.Any(p => p.PaymentStatus == "Оплачено") && ab.Id == amenityReview.AmenityBookingId);

                if (unreviewedCompletedBooking == null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость не может оставить отзыв для данного бронирования услуги");
                }

                amenityReview.PublicationDate = DateOnly.FromDateTime(DateTime.Now);
                amenityReview.PublicationTime = TimeOnly.FromDateTime(DateTime.Now);

                amenityReview.AmenityBookingId = unreviewedCompletedBooking.Id;

                await _context.AmenityReviews.AddAsync(amenityReview);
                await _context.SaveChangesAsync();

                return amenityReview;
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

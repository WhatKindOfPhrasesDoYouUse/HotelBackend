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

        public async Task<IEnumerable<AmenityReview>> GetAmenityReviewsByRoomId(long roomId)
        {
            try
            {
                var amenityReviews = await _context.AmenityReviews
                    .Include(g => g.Guest)
                        .ThenInclude(c => c.Client)
                    .Include(r => r.Amenity)
                        .ThenInclude(r => r.Room)
                    .Where(ar => ar.Amenity!.Room.Id == roomId)
                    .OrderByDescending(ar => ar.PublicationDate)
                    .ToListAsync();

                if (amenityReviews == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Отзывы об услуге отсутствуют");
                }

                return amenityReviews;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PagedResult<AmenityReview>> GetAmenityReviewsByRoomIdPages(long roomId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var query = _context.AmenityReviews
                    .Include(g => g.Guest)
                        .ThenInclude(c => c.Client)
                    .Include(r => r.Amenity)
                        .ThenInclude(r => r.Room)
                    .Where(ar => ar.Amenity!.Room.Id == roomId)
                    .OrderByDescending(ar => ar.PublicationDate)
                    .AsQueryable();

                var totalCount = await query.CountAsync();

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (items == null || items.Count == 0)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Отзывы об услугах отсутствуют");
                }

                PagedResult<AmenityReview> amenityReviewPages = new PagedResult<AmenityReview>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                if (amenityReviewPages == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Страницы не получены");
                }

                return amenityReviewPages;
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

        public async Task<bool> HasReviewForAmenityBooking(long bookingId)
        {
            try
            {
                if (bookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id услуги не может быть меньше или равно 0");
                }

                var amenityBooking = await _context.AmenityBookings.FindAsync(bookingId);

                if (amenityBooking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Бронирования дополнительной услуги с id: {bookingId} не существует");
                } 

                if (await _context.AmenityReviews.FirstOrDefaultAsync(ar => ar.AmenityBookingId == amenityBooking.Id) == null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public async Task<IEnumerable<AmenityReview>> GetAmenityReviews()
        {
            try
            {
                var amenityReviews = await _context.AmenityReviews.ToListAsync();

                if (amenityReviews == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Отзывы на дополнительные услуги");
                }

                return amenityReviews;
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

        public async Task DeleteAmenityReviewById(long amenityReviewId)
        {
            try
            {
                if (amenityReviewId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id услуги не может быть меньше или равно 0");
                }

                var amenityReview = await _context.AmenityReviews.FindAsync(amenityReviewId);

                if (amenityReview == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Отзыва на дополнительную услугу с id: {amenityReviewId} не существует");
                }

                _context.AmenityReviews.Remove(amenityReview);
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

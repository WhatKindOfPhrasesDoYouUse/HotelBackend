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
                        .ThenInclude(g => g.Client)
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

        public async Task SaveHotelReview(HotelReview hotelReview)
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

                var completedBookings = await _context.RoomBookings
                    .Where(rb => rb.GuestId == hotelReview.GuestId && rb.Room.HotelId == hotelReview.HotelId)
                    .Include(rb => rb.RoomPayments)
                    .ToListAsync();

                if (!completedBookings.Any(rb => rb.RoomPayments.Any()))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость не имеет завершённых бронирований в этом отеле");
                }

                if (hotelReview.Rating < 1 || hotelReview.Rating > 5)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Рейтинг должен быть в интервале от 1 до 5");
                }

                var reviewedBooking = await _context.HotelReviews
                    .Where(hr => hr.GuestId == hotelReview.GuestId && hr.HotelId == hotelReview.HotelId && hr.RoomBookingId == hotelReview.RoomBookingId)
                    .FirstOrDefaultAsync();

                if (reviewedBooking != null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость уже оставил отзыв для данного бронирования");
                }

                var unreviewedCompletedBooking = completedBookings
                    .FirstOrDefault(rb => rb.RoomPayments.Any() && rb.Id == hotelReview.RoomBookingId);

                if (unreviewedCompletedBooking == null)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Гость не может оставить отзыв для данного бронирования");
                }

                hotelReview.PublicationDate = DateOnly.FromDateTime(DateTime.Now);
                hotelReview.PublicationTime = TimeOnly.FromDateTime(DateTime.Now);

                hotelReview.RoomBookingId = unreviewedCompletedBooking.Id;

                await _context.HotelReviews.AddAsync(hotelReview);
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

        public async Task DeleteHotelReviewById(long hotelReviewId)
        {
            try
            {
                if (hotelReviewId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id отзыва не должно быть меньше или равно 0");
                }

                var hotelReview = await _context.HotelReviews.FindAsync(hotelReviewId);

                if (hotelReview == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Отзыв с id: {hotelReviewId} не найден");
                }

                _context.Remove(hotelReview);
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

        public async Task<HotelReview> UpdateHotelReview(long hotelReviewId, HotelReview newHotelReview)
        {
            try
            {
                if (hotelReviewId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id отзыва не должно быть меньше или равно 0");
                }

                var hotelReview = await _context.HotelReviews.FindAsync(hotelReviewId);

                if (hotelReview == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Отзыв с id: {hotelReviewId} не найден");
                }

                if (hotelReview.Rating < 1 || hotelReview.Rating > 5)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Рейтинг должен быть в интервале от 1 до 5");
                }

                if (!string.IsNullOrEmpty(newHotelReview.Comment))
                {
                    hotelReview.Comment = newHotelReview.Comment;
                }

                if (newHotelReview.Rating >= 1 && newHotelReview.Rating <= 5)
                {
                    hotelReview.Rating = newHotelReview.Rating;
                }

                hotelReview.PublicationDate = DateOnly.FromDateTime(DateTime.Now);
                hotelReview.PublicationTime = TimeOnly.FromDateTime(DateTime.Now);


                _context.HotelReviews.Update(hotelReview);
                await _context.SaveChangesAsync();

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

        public async Task<PagedResult<HotelReview>> GetHotelReviewPages(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var query = _context.HotelReviews
                    .Include(hr => hr.Guest)
                        .ThenInclude(g => g.Client)
                    .Include(hr => hr.Hotel)
                    .AsQueryable();

                var totalCount = await query.CountAsync();

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (items == null || items.Count == 0)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Отзывы об отеле отсутствуют");
                }

                PagedResult<HotelReview> hotelReviewPages = new PagedResult<HotelReview>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                if (hotelReviewPages == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Страницы не получены");
                }

                return hotelReviewPages;
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

        public async Task<PagedResult<HotelReview>> GetHotelReviewPagesByGuestId(long guestId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не должно быть меньше или равно 0");
                }

                var query = _context.HotelReviews
                    .Include(hr => hr.Guest)
                        .ThenInclude(g => g.Client)
                    .Include(hr => hr.Hotel)
                    .Where(hr => hr.GuestId == guestId)
                    .AsQueryable();

                var totalCount = await query.CountAsync();

                var items = await query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (items == null || items.Count == 0)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Отзывы гостя с id: {guestId} не найдены");
                }

                PagedResult<HotelReview> hotelReviewPages = new PagedResult<HotelReview>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                return hotelReviewPages;
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

        public async Task<int> GetHotelReviewCount()
        {
            try
            {
                int counter = await _context.HotelReviews.CountAsync();
                return counter;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<double> GetHotelReviewAvgRating()
        {
            try
            {
                if (!await _context.HotelReviews.AnyAsync())
                {
                    return 0.0;
                }

                double avgRating = await _context.HotelReviews.AverageAsync(r => r.Rating);
                return avgRating;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> HasReviewForBooking(long bookindId)
        {
            try
            {
                if (bookindId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id бронирования не должно быть меньше или равно 0");
                }

                var booking = await _context.RoomBookings.FindAsync(bookindId);

                if (booking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Бронирования с id: {bookindId} не существует");
                }

                if (await _context.HotelReviews.FirstOrDefaultAsync(hr => hr.RoomBookingId == bookindId) == null)
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
    }
}

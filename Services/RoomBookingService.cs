using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class RoomBookingService : IRoomBookingService
    {
        private readonly ApplicationDbContext _context;

        public RoomBookingService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<RoomBooking>> GetAllRoomBookings()
        {
            try
            {
                var roomBookings = await _context.RoomBookings.ToListAsync();

                if (roomBookings == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Ссылка на бронирования не найдена");
                }

                return roomBookings;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка со стороны сервера при получении данных бронирований комнат", ex);
            }
        }

        public async Task<IEnumerable<RoomBooking>> GetRoomBookingsByGuestId(long guestId)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть меньше или равно нулю");
                }
                
                if (await _context.Guests.FindAsync(guestId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {guestId} не найден");
                }

                var roomBookings = await _context.RoomBookings.Where(rb => rb.GuestId == guestId)
                    .Include(g => g.Guest)
                    .Include(r => r.Room)
                    .ToListAsync();

                if (roomBookings == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Ссылка на бронирования гостя с id: {guestId} не найдена");
                }

                return roomBookings;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, $"Произошла ошибка со стороны сервера при получении бронирований комнат гостя с id: {guestId}", ex);
            }
        }

        public async Task<IEnumerable<RoomBookingDto>> GetDetailedRoomBookingByGuestId(long guestId)
        {
            try
            {
                if (guestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть меньше или равно нулю");
                }

                if (await _context.Guests.FindAsync(guestId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {guestId} не найден");
                }

                var bookings = await _context.RoomBookings
                    .Include(rb => rb.Room)
                        .ThenInclude(r => r.Amenities)
                    .Where(rb => rb.GuestId == guestId)
                    .ToListAsync();

                if (!bookings.Any())
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Бронирования для гостя с id: {guestId} не найдены");
                }

                var paidBookingIds = await _context.RoomPayments
                    .Where(rp => bookings.Select(b => b.Id).Contains(rp.RoomBookingId))
                    .Select(rp => rp.RoomBookingId)
                    .ToListAsync();

                var bookingDtos = bookings.Select(booking =>
                {
                    var cancelUntilDate = booking.CheckInDate != null
                        ? booking.CheckInDate.AddDays(-1)
                        : (DateOnly?)null;

                    var cancelUntilTime = booking.CheckInTime != null
                        ? booking.CheckInTime
                        : new TimeOnly(14, 0);

                    return new RoomBookingDto
                    {
                        RoomBookingId = booking.Id,
                        CheckInDate = booking.CheckInDate,
                        CheckOutDate = booking.CheckOutDate,
                        CheckInTime = booking.CheckInTime,
                        CheckOutTime = booking.CheckOutTime,
                        Capacity = booking.Room?.Capacity,
                        NumberOfGuests = booking.NumberOfGuests,
                        UnitPrice = booking.Room?.UnitPrice,
                        RoomNumber = booking.Room?.RoomNumber,
                        CancelUntilDate = cancelUntilDate,
                        CancelUntilTime = cancelUntilTime,
                        IsPayd = paidBookingIds.Contains(booking.Id),
                        CreatedAt = booking.CreatedAt,
                        IsConfirmed = booking.IsConfirmed,
                        ConfirmationTime = booking.ConfirmationTime,
                        Amenities = booking.Room?.Amenities?.ToList() ?? new List<Amenity>()
                    };
                }).ToList();

                return bookingDtos;
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

        public async Task DeleteBookingById(long bookingId)
        {
            try
            {
                if (bookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id бронирования не может быть меньше или равно нулю");
                }

                var booking = await _context.RoomBookings
                    .Include(rp => rp.RoomPayments)
                    .FirstOrDefaultAsync(b => b.Id == bookingId);

                if (booking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Бронирование с id: {bookingId} не найдено");
                }

                if (booking.CheckInDate == default)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Дата заезда не установлена.");
                }

                var cancelUntilDate = booking.CheckInDate.AddDays(-1);
                var cancelUntilTime = booking.CheckInTime == default ? new TimeOnly(14, 0) : booking.CheckInTime;

                var currentDateTime = DateTime.UtcNow;
                var cancelUntilDateTime = cancelUntilDate.ToDateTime(cancelUntilTime);

                if (currentDateTime > cancelUntilDateTime)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Срок для отмены бронирования прошел.");
                }

                var payment = await _context.RoomPayments.FirstOrDefaultAsync(rp => rp.RoomBookingId == booking.Id);

                if (payment != null)
                {
                    _context.RoomPayments.Remove(payment);
                }

                var review = await _context.HotelReviews.FirstOrDefaultAsync(hr => hr.RoomBookingId == booking.Id);

                if (review != null)
                {
                    _context.HotelReviews.Remove(review);
                }

                var amenityBookings = await _context.AmenityBookings
                    .Include(ap => ap.AmenityPayments)
                    .Include(ar => ar.AmenityReview)
                    .Where(ab => ab.RoomBookingId == booking.Id).ToListAsync();

                if (amenityBookings.Any() || amenityBookings != null)
                {
                    _context.AmenityBookings.RemoveRange(amenityBookings);
                }

                foreach (var amenityBooking in amenityBookings!)
                {
                    if (amenityBooking.AmenityPayments.Any())
                    {
                        _context.AmenityPayments.RemoveRange(amenityBooking.AmenityPayments);
                    }

                    if (amenityBooking.AmenityReview != null)
                    {
                        _context.AmenityReviews.Remove(amenityBooking.AmenityReview);
                    }
                }

                _context.RoomBookings.Remove(booking);
                await _context.SaveChangesAsync();
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

        public async Task<RoomBooking> SaveSingleRoomBooking(RoomBooking roomBooking)
        {
            try
            {
                if (roomBooking.GuestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть меньше или равен 0");
                }

                if (await _context.Guests.FindAsync(roomBooking.GuestId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {roomBooking.GuestId} не найден");
                }

                if (roomBooking.RoomId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id комнаты не может быть меньше или равно нулю");
                }

                if (await _context.Rooms.FindAsync(roomBooking.RoomId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomBooking.RoomId} не найден");
                }

                if (roomBooking.CheckInDate.ToDateTime(roomBooking.CheckInTime) >= roomBooking.CheckOutDate.ToDateTime(roomBooking.CheckOutTime))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Дата и время заезда должны быть позде даты и времени бронирования");
                }

                if (roomBooking.CheckInDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Дата заселения не может быть раньше текущей даты");
                }

                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomBooking.RoomId);

                if (room == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комнаты с id: {roomBooking.RoomId} не существует");
                }

                if (room.Capacity > 1)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Комната расчитана на 1 гостя");
                }

                if (roomBooking.NumberOfGuests > room.Capacity)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"Количество желаемых мест: {roomBooking.NumberOfGuests} должно быть меньше {room.Capacity}");
                }

                bool isOverlappingBookingExists = await _context.RoomBookings.
                    AnyAsync(rb => rb.RoomId == roomBooking.RoomId
                    && rb.Id != roomBooking.Id
                    && roomBooking.CheckInDate <= rb.CheckOutDate && roomBooking.CheckOutDate >= rb.CheckInDate);

                if (isOverlappingBookingExists)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Комната уже забронирована на указанный период");
                }

                roomBooking.AdditionalGuests = null;
                roomBooking.CreatedAt = DateTime.UtcNow;

                _context.RoomBookings.Add(roomBooking);
                await _context.SaveChangesAsync();

                return roomBooking;
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

        public async Task<RoomBooking> SaveGroupRoomBooking(RoomBooking roomBooking)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (roomBooking.GuestId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id гостя не может быть меньше или равен 0");
                }

                var mainGuest = await _context.Guests.FindAsync(roomBooking.GuestId);
                if (mainGuest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гость с id: {roomBooking.GuestId} не найден");
                }

                if (roomBooking.RoomId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id комнаты не может быть меньше или равно нулю");
                }

                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomBooking.RoomId);
                if (room == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {roomBooking.RoomId} не найдена");
                }

                if (room.Capacity < roomBooking.NumberOfGuests)
                {
                    throw new ServiceException(ErrorCode.BadRequest,
                        $"Комната расчитана на {room.Capacity} гостей, а запрошено {roomBooking.NumberOfGuests}");
                }

                var checkInDateTime = roomBooking.CheckInDate.ToDateTime(roomBooking.CheckInTime);
                var checkOutDateTime = roomBooking.CheckOutDate.ToDateTime(roomBooking.CheckOutTime);

                if (checkInDateTime >= checkOutDateTime)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Дата и время заезда должны быть раньше даты и времени выезда");
                }

                if (roomBooking.CheckInDate < DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Дата заезда не может быть раньше текущей даты");
                }

                bool isOverlappingBookingExists = await _context.RoomBookings.AnyAsync(rb =>
                    rb.RoomId == roomBooking.RoomId &&
                    rb.Id != roomBooking.Id &&
                    roomBooking.CheckInDate <= rb.CheckOutDate &&
                    roomBooking.CheckOutDate >= rb.CheckInDate);

                if (isOverlappingBookingExists)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Комната уже забронирована на указанный период");
                }

                var additionalGuests = roomBooking.AdditionalGuests?.ToList();
                roomBooking.AdditionalGuests = null;
                roomBooking.CreatedAt = DateTime.UtcNow;

                _context.RoomBookings.Add(roomBooking);
                await _context.SaveChangesAsync();

                if (additionalGuests != null && additionalGuests.Any())
                {
                    foreach (var guest in additionalGuests)
                    {
                        guest.RoomBookingId = roomBooking.Id;
                        guest.DateOfBirth = DateTime.SpecifyKind(guest.DateOfBirth, DateTimeKind.Utc);
                    }

                    _context.AdditionalGuests.AddRange(additionalGuests);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();
                roomBooking.AdditionalGuests = additionalGuests;

                return roomBooking;
            }
            catch (ServiceException)
            {
                await transaction.RollbackAsync();
                throw;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<RoomBooking> ConfirmSingleRoomBooking(long bookingId)
        {
            try
            {
                if (bookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id бронирования не может быть меньше или равно нулю");
                }

                var booking = await _context.RoomBookings.FindAsync(bookingId);

                if (booking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Бронирование с id: {bookingId} не найдено");
                }

                booking.IsConfirmed = true;
                booking.ConfirmationTime = DateTime.UtcNow;

                _context.RoomBookings.Update(booking);
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

        public async Task UpdateSingleRoomBookingId(long bookingId, UpdateRoomBookingDto updateRoomBookingDto)
        {
            try
            {
                if (bookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id бронирования не может быть меньше или равно нулю");
                }
                var booking = await _context.RoomBookings.FindAsync(bookingId);

                if (booking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Бронирование с id: {bookingId} не найдено");
                }

                if (booking.IsConfirmed)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Нельзя изменить подтвержденное бронирование");
                }

                var checkInDateTime = booking.CheckInDate.ToDateTime(booking.CheckInTime);

                if (DateTime.UtcNow.AddHours(20) > checkInDateTime)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Изменения запрещены менее чем за 24 часа до заезда.");
                }

                if (updateRoomBookingDto.CheckInDate.HasValue)
                    booking.CheckInDate = updateRoomBookingDto.CheckInDate.Value;

                if (updateRoomBookingDto.CheckOutDate.HasValue)
                    booking.CheckOutDate = updateRoomBookingDto.CheckOutDate.Value;

                if (updateRoomBookingDto.CheckInTime.HasValue)
                    booking.CheckInTime = updateRoomBookingDto.CheckInTime.Value;

                if (updateRoomBookingDto.CheckOutTime.HasValue)
                    booking.CheckOutTime = updateRoomBookingDto.CheckOutTime.Value;

                booking.NumberOfGuests = 1;

                _context.RoomBookings.Update(booking);
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

        public async Task UpdateAdditionalGuestByRoomBookingId(long bookingId, List<AdditionalGuest> additionalGuests)
        {
            try
            {
                if (bookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id бронирования должен быть положительным числом");
                }

                var booking = await _context.RoomBookings
                    .Include(b => b.AdditionalGuests)
                    .FirstOrDefaultAsync(b => b.Id == bookingId);

                if (booking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Бронирование с id: {bookingId} не найдено");
                }

                var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == booking.RoomId);
                if (room == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комната с id: {booking.RoomId} не найдена");
                }

                int totalGuests = additionalGuests?.Count + 1 ?? 1;
                if (totalGuests > room.Capacity)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"Количество гостей ({totalGuests}) превышает вместимость комнаты ({room.Capacity})");
                }

                _context.AdditionalGuests.RemoveRange(booking.AdditionalGuests);

                if (additionalGuests != null && additionalGuests.Any())
                {
                    foreach (var guest in additionalGuests)
                    {
                        guest.RoomBookingId = bookingId;
                    }
                    await _context.AdditionalGuests.AddRangeAsync(additionalGuests);
                }

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

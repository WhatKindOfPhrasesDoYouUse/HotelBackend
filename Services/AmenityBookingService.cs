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
                    CompletionStatus = "В ожидании подтверждения",
                    EmployeeId = null,
                    RoomBookingId = amenityBookingDto.RoomBookingId
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

        public async Task<IEnumerable<AmenityBooking>> GetAmenityBookings(long bookindRoomId)
        {
            try
            {
                if (bookindRoomId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, $"id бронирования ${bookindRoomId} не может быть меньше или равно 0");
                }

                var amenityBookings = await _context.AmenityBookings
                    .Where(ab => ab.RoomBookingId == bookindRoomId)
                    .ToListAsync();

                if (amenityBookings == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Данные забронированных дополнительных услуг не найдены");
                }

                return amenityBookings;
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

        public async Task<AmenityBooking> TakeAmenityTask(long amenityBookingId, long employeeId)
        {
            try
            {
                if (amenityBookingId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id заказа дополнительной услуги не может быть меньше или равно 0");
                }

                var amenityBooking = await _context.AmenityBookings
                    .Include(a => a.Amenity)
                        .ThenInclude(et => et.EmployeeType)
                    .FirstOrDefaultAsync(ab => ab.Id == amenityBookingId);

                if (amenityBooking == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Заказанная услуга с id: {amenityBookingId} не нейдена");
                }

                if (employeeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id сотрудника не может быть меньше или равно 0");
                }

                if (amenityBooking.CompletionStatus != "В ожидании подтверждения")
                {
                    throw new ServiceException(ErrorCode.Conflict, "Заказ уже в процессе выполнения");
                }

                var employee = await _context.Employees
                    .Include(et => et.EmployeeType)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);

                if (employee == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Соотрудник с id: ${employeeId} не найдена");
                }

                var amenity = await _context.Amenities.FindAsync(amenityBooking.AmenityId);

                if (amenity == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Услуги с id: ${amenityBooking.AmenityId} не существует");
                }

                amenityBooking.EmployeeId = employee.Id;
                amenityBooking.CompletionStatus = "В процессе выполнения";

                if (amenity.EmployeeTypeId != employee.EmployeeTypeId)
                {
                    amenityBooking.EmployeeId = null;
                    amenityBooking.CompletionStatus = "В ожидании подтверждения";

                    throw new ServiceException(ErrorCode.NotFound, "Этот пользователь не должен брать задачи данного типа");
                }

                _context.AmenityBookings.Update(amenityBooking);
                await _context.SaveChangesAsync();

                return amenityBooking;
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

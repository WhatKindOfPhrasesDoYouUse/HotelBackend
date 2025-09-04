using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class HotelTypeService : IHotelTypeService
    {
        private readonly ApplicationDbContext _context;

        public HotelTypeService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<HotelType>> GetHotelTypes()
        {
            try
            {
                var hotelTypes = await _context.HotelTypes.ToListAsync();

                if (hotelTypes == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Типы отелей не найдены");
                }

                return hotelTypes;
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

        public async Task DeleteHotelTypeById(long hotelTypeId)
        {
            try
            {
                if (hotelTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id типа отеля не может быть меньше или равен 0");
                }

                var hotelType = await _context.HotelTypes
                    .Include(h => h.Hotels)
                    .FirstOrDefaultAsync(ht => ht.Id == hotelTypeId);

                if (hotelType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип отеля с id: {hotelTypeId} не найден");
                }

                if (hotelType.Hotels != null)
                {
                    foreach (Hotel hotel in hotelType.Hotels)
                    {
                        hotel.HotelTypeId = null;
                    }
                }

                _context.HotelTypes.Remove(hotelType);
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

        public async Task SaveHotelType(HotelType hotelType)
        {
            try
            {
                var existingHotelType = await _context.HotelTypes.FirstOrDefaultAsync(ht => ht.Name == hotelType.Name);

                if (existingHotelType != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Такой тип гостиницы уже существует");
                }

                await _context.HotelTypes.AddAsync(hotelType);
                await _context.SaveChangesAsync(true);
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

        public async Task<HotelType> GetHotelTypeById(long hotelTypeId)
        {
            try
            {
                var hotelType = await _context.HotelTypes.FindAsync(hotelTypeId);

                if (hotelType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип гостиницы с id: {hotelTypeId} не найден");
                }

                return hotelType;
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

        public async Task UpdateHotelTypeById(long hotelTypeId, HotelType newHotelType)
        {
            try
            {
                var hotelType = await _context.HotelTypes.FindAsync(hotelTypeId);

                if (hotelType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип гостиницы с id: {hotelTypeId} не найден");
                }


                if (!string.IsNullOrEmpty(newHotelType.Name))
                    hotelType.Name = newHotelType.Name;

                if (!string.IsNullOrEmpty(newHotelType.Description))
                    hotelType.Description = newHotelType.Description;

                _context.HotelTypes.Update(hotelType);
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

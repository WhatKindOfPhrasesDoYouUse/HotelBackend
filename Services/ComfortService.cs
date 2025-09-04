using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class ComfortService : IComfortService
    {
        private readonly ApplicationDbContext _context;

        public ComfortService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<Comfort>> GetAllComforts()
        {
            try
            {
                var comforts = await _context.Comforts.ToListAsync();

                if (comforts == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Комфортности не найдены");
                }

                return comforts;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении отелей со стороны сервера", ex);
            }
        }

        public async Task DeleteComfortById(long comfortId)
        {
            try
            {
                if (comfortId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id комфорта не может быть меньше или равно 0");
                }

                var comfort = await _context.Comforts
                    .Include(rc => rc.RoomComforts)
                    .FirstOrDefaultAsync(c => c.Id == comfortId);

                if (comfort == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комфорт c id: {comfortId} не найден");
                }

                _context.RoomsComforts.RemoveRange(comfort.RoomComforts);
                _context.Comforts.Remove(comfort);

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

        public async Task SaveComfort(Comfort comfort)
        {
            try
            {
                var existingComfort = await _context.Comforts.FirstOrDefaultAsync(c => c.Name == comfort.Name);

                if (existingComfort != null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Такой комфорт уже есть");
                }

                await _context.Comforts.AddAsync(comfort);
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

        public async Task<Comfort> GetComfortById(long comfortId)
        {
            try
            {
                if (comfortId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id комфорта не может быть меньше или равно 0");
                }

                var comfort = await _context.Comforts
                    .FirstOrDefaultAsync(c => c.Id == comfortId);

                if (comfort == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Комфорт c id: {comfortId} не найден");
                }

                return comfort;
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

        public async Task UpdateComfortById(long comfortId, Comfort newComfort)
        {
            if (comfortId <= 0)
            {
                throw new ServiceException(ErrorCode.BadRequest, "Id комфорта не может быть меньше или равно 0");
            }

            var existingComfort = await _context.Comforts
                .FirstOrDefaultAsync(c => c.Id == comfortId);

            if (existingComfort == null)
            {
                throw new ServiceException(ErrorCode.NotFound, "Комфорт с указанным ID не найден");
            }

            var comfortWithSameName = await _context.Comforts
                .FirstOrDefaultAsync(c => c.Name == newComfort.Name && c.Id != comfortId);

            if (comfortWithSameName != null)
            {
                throw new ServiceException(ErrorCode.Conflict, "Такой комфорт уже существует");
            }

            if (!string.IsNullOrEmpty(newComfort.Name))
            {
                existingComfort.Name = newComfort.Name;
            }

            _context.Comforts.Update(existingComfort);
            await _context.SaveChangesAsync();
        }
    }
}

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
    }
}

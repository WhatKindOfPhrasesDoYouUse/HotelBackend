using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class BankService : IBankService
    {
        private readonly ApplicationDbContext _context;

        public BankService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<Bank>> GetAllBanks()
        {
            try
            {
                var banks = await _context.Banks.ToListAsync();

                if (banks == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Список банков не найден");
                }

                return banks;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении данных о банках со стороны сервера", ex);
            }
        }
    }
}

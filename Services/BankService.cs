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

        public async Task DeleteBankById(long bankId)
        {
            try
            {
                if (bankId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id банка меньше или равно 0");
                }

                var bank = await _context.Banks
                    .Include(c => c.Cards)
                    .FirstOrDefaultAsync(b => b.Id == bankId);

                if (bank == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Банка с id: {bankId} не найден");
                }

                _context.Banks.Remove(bank);
                _context.Cards.RemoveRange(bank.Cards);
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

        public async Task SaveBank(Bank bank)
        {
            try
            {
                var existingBank = await _context.Banks.FirstOrDefaultAsync(b => b.Name == bank.Name);

                if (existingBank != null)
                {
                    throw new ServiceException(ErrorCode.Conflict, "Такой банк уже существует");
                }

                await _context.Banks.AddAsync(bank);
                await _context.SaveChangesAsync();
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<Bank> GetBankById(long bankId)
        {
            try
            {
                if (bankId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id банка меньше или равно 0");
                }

                var bank = await _context.Banks.FindAsync(bankId);

                if (bank == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Банка с id: {bankId} не найден");
                }

                return bank;
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

        public async Task UpdateBankById(long bankId, Bank newBank)
        {
            try
            {
                var bank = await _context.Banks.FindAsync(bankId);

                if (bank == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Банк с id: {bankId} не найден");
                }


                if (!string.IsNullOrEmpty(newBank.Name))
                    bank.Name = newBank.Name;

                _context.Banks.Update(bank);
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

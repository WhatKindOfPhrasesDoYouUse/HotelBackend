using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IBankService
    {
        Task<IEnumerable<Bank>> GetAllBanks();
        Task DeleteBankById(long bankId);
        Task SaveBank(Bank bank);
        Task<Bank> GetBankById(long bankId);
        Task UpdateBankById(long bankId, Bank newBank);
    }
}

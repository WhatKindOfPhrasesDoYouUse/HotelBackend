using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IBankService
    {
        Task<IEnumerable<Bank>> GetAllBanks();
    }
}

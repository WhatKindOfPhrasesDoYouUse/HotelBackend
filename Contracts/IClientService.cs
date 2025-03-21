using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IClientService
    {
        Task<Client> GetGuestByClientId(long clientId);
    }
}

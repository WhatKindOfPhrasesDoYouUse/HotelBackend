using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IClientService
    {
        Task<Client> GetGuestByClientId(long clientId);
        Task<Client> UpdateClientGuest(long clientId, UpdateClientGuestDto updateClientGuestDto);
        Task<string> UpdatePassword(long clientId, UpdatePasswordDto updatePasswordDto);
    }
}

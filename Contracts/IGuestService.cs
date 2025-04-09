using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IGuestService
    {
        Task<Guest> BindCardToGuest(long clientId, long cardId);
        Task<Guest> GetGuestByClientId(long clientId);
    }
}

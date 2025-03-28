using HotelBackend.DataTransferObjects;
using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface ICardService
    {
        Task<Card> CreateAndAndAttachCardToGuest(CreateCardAndAttachDto createCardAndAttachDto);
    }
}

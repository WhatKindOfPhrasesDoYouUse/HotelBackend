using HotelBackend.DataTransferObjects;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Contracts
{
    public interface ICardService
    {
        Task<Card> CreateCard(Card card);
        Task<CardDto> GetCardByGuestId(long guestId);
        Task<CardDto> GetCardById(long cardId);
        Task DeleteCardById(long cardId);
        Task UpdateCard(long cardId, CardDto cardDto);
        Task<IEnumerable<Card>> GetAllCards();
    }
}

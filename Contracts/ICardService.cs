using HotelBackend.DataTransferObjects;
using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Contracts
{
    public interface ICardService
    {
        Task<Card> CreateCard(Card card);
        Task<CardDto> GetCardByGuestId(long guestId);
        Task DeleteCardById(long cardId);
    }
}

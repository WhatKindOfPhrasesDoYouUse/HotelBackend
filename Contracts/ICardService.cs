using HotelBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBackend.Contracts
{
    public interface ICardService
    {
        Task<Card> CreateCard(Card card);
    }
}

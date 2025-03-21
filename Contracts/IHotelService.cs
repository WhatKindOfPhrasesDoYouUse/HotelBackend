using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IHotelService
    {
        Task<IEnumerable<Hotel>> GetAllHotels();
    }
}

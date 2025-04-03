using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IComfortService
    {
        Task<IEnumerable<Comfort>> GetAllComforts();
    }
}

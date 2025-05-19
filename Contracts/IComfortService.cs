using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IComfortService
    {
        Task<IEnumerable<Comfort>> GetAllComforts();
        Task DeleteComfortById(long comfortId);
        Task SaveComfort(Comfort comfort);
        Task<Comfort> GetComfortById(long comfortId);
        Task UpdateComfortById(long comfortId, Comfort newComfort);
    }
}

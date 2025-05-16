using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IHotelTypeService
    {
        Task<IEnumerable<HotelType>> GetHotelTypes();
        Task DeleteHotelTypeById(long hotelTypeId);
        Task SaveHotelType(HotelType hotelType);
        Task<HotelType> GetHotelTypeById(long hotelTypeId);
        Task UpdateHotelTypeById(long hotelTypeId,  HotelType newHotelType);
    }
}

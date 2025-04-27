using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IEmployeeTypeService
    {
        Task<IEnumerable<EmployeeType>> GetEmployeeTypes();
    }
}

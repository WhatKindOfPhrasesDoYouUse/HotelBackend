using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IEmployeeTypeService
    {
        Task<IEnumerable<EmployeeType>> GetEmployeeTypes();
        Task SaveEmployeeType(EmployeeType employeeType);
        Task DeleteEmployeeTypeById(long employeeTypeId);
        Task<EmployeeType> GetEmployeeTypeById(long employeeTypeId);
        Task UpdateEmployeeTypeById(long employeeTypeId, EmployeeType newEmployeeType);
    }
}

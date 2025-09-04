using HotelBackend.Models;

namespace HotelBackend.Contracts
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task DeleteEmployeeById(long employeeId);
        Task UpdateEmployeeById(long employeeId, Employee newEmployee);
        Task<Employee> GetEmployeeById(long employeeId);
    }
}

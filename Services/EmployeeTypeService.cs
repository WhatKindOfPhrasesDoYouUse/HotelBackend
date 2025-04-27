using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeTypeService(ApplicationDbContext context) => this._context = context;
        
        public async Task<IEnumerable<EmployeeType>> GetEmployeeTypes()
        {
            try
            {
                var employeeTypes = await _context.EmployeeTypes.ToListAsync();

                if (employeeTypes == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Типы сотрудников не найдены");
                }

                return employeeTypes;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

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

        public async Task SaveEmployeeType(EmployeeType employeeType)
        {
            try
            {
                var existingEmployeeType = await _context.EmployeeTypes
                    .FirstOrDefaultAsync(et => et.Name == employeeType.Name);

                if (existingEmployeeType != null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Такой тип сотрудника уже существует");
                }

                await _context.EmployeeTypes.AddAsync(employeeType);
                await _context.SaveChangesAsync();
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

        public async Task DeleteEmployeeTypeById(long employeeTypeId)
        {
            try
            {
                if (employeeTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id типа сотрудника не может быть меньше или равен нулю");
                }

                var employeeType = await _context.EmployeeTypes.FindAsync(employeeTypeId);

                if (employeeType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип отеля с id: {employeeTypeId} не найден");
                }

                _context.EmployeeTypes.Remove(employeeType);
                await _context.SaveChangesAsync();
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

        public async Task<EmployeeType> GetEmployeeTypeById(long employeeTypeId)
        {
            try
            {
                if (employeeTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id типа сотрудника не может быть меньше или равен нулю");
                }

                var employeeType = await _context.EmployeeTypes.FindAsync(employeeTypeId);

                if (employeeType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип отеля с id: {employeeTypeId} не найден");
                }

                return employeeType;
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

        public async Task UpdateEmployeeTypeById(long employeeTypeId, EmployeeType newEmployeeType)
        {
            try
            {
                if (employeeTypeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id типа сотрудника не может быть меньше или равен нулю");
                }

                var employeeType = await _context.EmployeeTypes.FindAsync(employeeTypeId);

                if (employeeType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип отеля с id: {employeeTypeId} не найден");
                }

                if (!string.IsNullOrEmpty(newEmployeeType.Name))
                    employeeType.Name = newEmployeeType.Name;

                _context.EmployeeTypes.Update(employeeType);
                await _context.SaveChangesAsync();
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

using HotelBackend.Contracts;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context) => this._context = context;

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                var employees = await _context.Employees
                    .Include(c => c.Client)
                    .Include(et => et.EmployeeType)
                    .ToListAsync();

                if (employees == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, "Список сотрудников не найден");
                }

                return employees;
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

        public async Task<Employee> GetEmployeeById(long employeeId)
        {
            try
            {
                var employee = await _context.Employees
                    .Include(c => c.Client)
                    .Include(et => et.EmployeeType)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);

                if (employee == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Сотрудника с id: ${employeeId} не найдено");
                }

                return employee;
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

        public async Task DeleteEmployeeById(long employeeId)
        {
            try
            {
                if (employeeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id сотрудника не может быть меньше или равно 0");
                }

                var employee = await _context.Employees
                    .Include(ab => ab.AmenityBookings)
                    .Include(c => c.Client)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);

                if (employee == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Сотрудника с id: {employeeId} не существует");
                }

                if (employee.AmenityBookings != null)
                {
                    _context.AmenityBookings.RemoveRange(employee.AmenityBookings);
                }

                _context.Clients.Remove(employee.Client);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task UpdateEmployeeById(long employeeId, Employee newEmployee)
        {
            try
            {
                if (employeeId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Id сотрудника не может быть меньше или равно 0");
                }

                var employee = await _context.Employees
                    .Include(c => c.Client)
                    .FirstOrDefaultAsync(e => e.Id == employeeId);

                if (employee == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Сотрудник с id: {employeeId} не найден");
                }

                if (!string.IsNullOrEmpty(newEmployee.Client!.Name))
                {
                    employee.Client!.Name = newEmployee.Client.Name;
                }

                if (!string.IsNullOrEmpty(newEmployee.Client.Surname))
                {
                    employee.Client!.Surname = newEmployee.Client.Surname;
                }

                if (!string.IsNullOrEmpty(newEmployee.Client.Patronymic))
                {
                    employee.Client!.Patronymic = newEmployee.Client.Patronymic;
                }

                if (!string.IsNullOrEmpty(newEmployee.Client.Email))
                {
                    employee.Client!.Email = newEmployee.Client.Email;
                }

                if (!string.IsNullOrEmpty(newEmployee.Client.PasswordHash))
                {
                    employee.Client!.PasswordHash = newEmployee.Client.PasswordHash;
                }

                if (employee.EmployeeTypeId != default)
                {
                    employee.EmployeeTypeId = newEmployee.EmployeeTypeId;
                }

                employee.HotelId = newEmployee.HotelId;
                employee.Client!.PasswordHash = newEmployee.Client.PasswordHash;

                _context.Employees.Update(employee);
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

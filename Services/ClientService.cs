using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Client> _passwordHasher;

        public ClientService(ApplicationDbContext context, PasswordHasher<Client> passwordHasher)
        {
            this._context = context;
            this._passwordHasher = passwordHasher;
        }

        public string HashPassword(Client client, string password)
        {
            return _passwordHasher.HashPassword(client, password);
        }

        public bool VerifyPassword(Client client, string password)
        {
            var verificationResult = _passwordHasher.VerifyHashedPassword(client, client.PasswordHash, password);
            return verificationResult == PasswordVerificationResult.Success;
        }

        public bool IsSamePassword(Client client, string newPassword)
        {
            return _passwordHasher.VerifyHashedPassword(client, client.PasswordHash, newPassword)
                == PasswordVerificationResult.Success;
        }

        public async Task<Client> GetGuestByClientId(long clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id клиента не может быть отрицательным значением");
                }

                var client = await _context.Clients
                    .Include(c => c.Guest)
                    .FirstOrDefaultAsync(c => c.Id == clientId);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гостя с таким clientId: {clientId} не существует");
                }

                return client;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при получении гостя со стороны сервера", ex);
            }
        }

        public async Task<Client> UpdateClientGuest(long clientId, UpdateClientGuestDto updateClientGuestDto)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id клиента не может быть отрицательным значением");
                }

                var client = await _context.Clients.Include(g => g.Guest).FirstOrDefaultAsync(c => c.Id == clientId);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Клиента с таким id: {clientId} не существует");
                }

                if (!string.IsNullOrEmpty(updateClientGuestDto.Name)) 
                    client.Name = updateClientGuestDto.Name;

                if (!string.IsNullOrEmpty(updateClientGuestDto.Surname)) 
                    client.Surname = updateClientGuestDto.Surname;

                if (!string.IsNullOrEmpty(updateClientGuestDto.Patronymic)) 
                    client.Patronymic = updateClientGuestDto.Patronymic;

                if (!string.IsNullOrEmpty(updateClientGuestDto.Email)) 
                    client.Email = updateClientGuestDto.Email;

                if (!string.IsNullOrEmpty(updateClientGuestDto.PhoneNumber)) 
                    client.PhoneNumber = updateClientGuestDto.PhoneNumber;

                if (client.Guest != null)
                {
                    if (!string.IsNullOrEmpty(updateClientGuestDto.CityOfResidence))
                        client.Guest.CityOfResidence = updateClientGuestDto.CityOfResidence;

                    if (updateClientGuestDto.DateOfBirth.HasValue)
                        client.Guest.DateOfBirth = updateClientGuestDto.DateOfBirth.Value.ToUniversalTime();

                    if (!string.IsNullOrEmpty(updateClientGuestDto.PassportSeriesHash))
                        client.Guest.PassportSeriesHash = updateClientGuestDto.PassportSeriesHash;

                    if (!string.IsNullOrEmpty(updateClientGuestDto.PassportNumberHash))
                        client.Guest.PassportNumberHash = updateClientGuestDto.PassportNumberHash;

                    if (!string.IsNullOrEmpty(updateClientGuestDto.LoyaltyStatus))
                        client.Guest.LoyaltyStatus = updateClientGuestDto.LoyaltyStatus;
                }

                await _context.SaveChangesAsync();

                return client;
            }
            catch (ServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, ex.Message);
            }
        }

        public async Task<string> UpdatePassword(long clientId, UpdatePasswordDto updatePasswordDto)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id клиента не может быть отрицательным значением");
                }

                var client = await _context.Clients.Include(g => g.Guest).FirstOrDefaultAsync(c => c.Id == clientId);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Клиента с таким id: {clientId} не существует");
                }

                if (!VerifyPassword(client, updatePasswordDto.OldPassword))
                {
                    throw new ServiceException(ErrorCode.Unauthorized, "Старый пароль введен неверно");
                }

                if (IsSamePassword(client, updatePasswordDto.NewPassword))
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Новый пароль не должен совпадать с текущим");
                }

                if (updatePasswordDto.NewPassword != updatePasswordDto.ConfirmPassword)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Новый пароль и подтверждение пароля не совпадают");
                }

                client.PasswordHash = HashPassword(client, updatePasswordDto.NewPassword);
                await _context.SaveChangesAsync();

                return "Пароль успешно обновлен";
            } 
            catch (ServiceException) 
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при обновлении пароля.", ex);
            }
        }

        public async Task DeleteClientByGuestId(long guestId)
        {
            try
            {
                var guest = await _context.Guests.FindAsync(guestId);

                if (guest == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Гостя с id: {guestId} не существует");
                }

                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == guest.ClientId);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Клиента с id: {guest.ClientId} не существует");
                }

                _context.Clients.Remove(client);
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

        public async Task<EmployeeDto> GetEmployeeByClientId(long clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id клиента не может быть меньше или равно 0");
                }

                var client = await _context.Clients.FindAsync(clientId);

                if (client == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Клиент с id: {clientId} не найден");
                }

                var employee = await _context.Employees
                    .Include(et => et.EmployeeType)
                    .FirstOrDefaultAsync(e => e.ClientId == client.Id);

                if (employee == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Сотрудник для клиента с id: {clientId} не найден");
                }

                if (employee.EmployeeType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Тип сотрудника не найден для сотрудника с id: {employee.Id}");
                }

                if (employee.EmployeeType.Name == "guest")
                {
                    throw new ServiceException(ErrorCode.BadRequest, "Это гость");
                }

                EmployeeDto employeeDto = new EmployeeDto
                {
                    Id = employee.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                    Patronymic = client.Patronymic,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    Role = employee.EmployeeType.Name
                };

                return employeeDto;
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

        public async Task<EmployeeType> GetEmployeeTypeByClientId(long clientId)
        {
            try
            {
                if (clientId <= 0)
                {
                    throw new ServiceException(ErrorCode.BadRequest, "id клиента не может быть меньше или равно 0");
                }

                if (await _context.Clients.FindAsync(clientId) == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Не существует клиента с id: {clientId}");
                }

                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ClientId == clientId);

                if (employee == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Не существует сотрудника с id клиента: {clientId}");
                }

                var employeeType = await _context.EmployeeTypes.FirstOrDefaultAsync(et => et.Id == employee.EmployeeTypeId);

                if (employeeType == null)
                {
                    throw new ServiceException(ErrorCode.NotFound, $"Не существует типа сотрудника с id: {employee.EmployeeTypeId}");
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
    }
}

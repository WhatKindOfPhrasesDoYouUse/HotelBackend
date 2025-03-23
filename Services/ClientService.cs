using HotelBackend.Contracts;
using HotelBackend.DataTransferObjects;
using HotelBackend.Exceptions;
using HotelBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBackend.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context) => this._context = context;

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
                        client.Guest.DateOfBirth = updateClientGuestDto.DateOfBirth.Value;

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
            catch (ServiceException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ServiceException(ErrorCode.InternalServerError, "Произошла ошибка при обновлении данных клиента.", ex);
            }
        }
    }
}
